let games = [];
let developers = [];
let publishers = [];
let connection = null;
let mostprofitablegame = [];
let gamerevenueinfo = [];
let averagepriceofgames = -1;
let numberofgamespertype = [];
let gamesofpublisher = [];
let gamesofdeveloper = [];

let gameidtoupdate = -1;
let devidtoupdate = -1;
let publisheridtoupdate = -1;


getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:54503/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GameCreated", (user, message) => {
        getdata();
    });

    connection.on("GameDeleted", (user, message) => {
        getdata();
    });

    connection.on("GameUpdated", (user, message) => {
        getdata();
    });

    connection.on("DeveloperCreated", (user, message) => {
        getdata();
    });

    connection.on("DeveloperUpdated", (user, message) => {
        getdata();
    });

    connection.on("DeveloperDeleted", (user, message) => {
        getdata();
    });

    connection.on("PublisherCreated", (user, message) => {
        getdata();
    });

    connection.on("PublisherUpdated", (user, message) => {
        getdata();
    });

    connection.on("PublisherDeleted", (user, message) => {
        getdata();
    });

    connection.on("GetGamesOfPublisher", (user, message) => {
        getdata();
    });
    connection.on("GetGamesOfDevelopers", (user, message) => {
        getdata();
    });


    connection.onclose(async () => {
        await start();
    });
    start();

}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}


async function getdata() {
    await fetch('http://localhost:54503/game')
    .then(x => x.json())
    .then(y => {
        games = y;
        display();
    });

    await fetch('http://localhost:54503/developer')
    .then(x => x.json())
    .then(y => {
        developers = y;
        display();
    });

    await fetch('http://localhost:54503/publisher')
    .then(x => x.json())
    .then(y => {
        publishers = y;
        display();
    });

    await fetch('http://localhost:54503/stat/mostprofitablegame')
    .then(x => x.json())
    .then(y => {
        mostprofitablegame = y;
        display();
    });

    await fetch('http://localhost:54503/stat/gamerevenueinfo')
    .then(x => x.json())
    .then(y => {
        gamerevenueinfo = y;
        display();
    });
    await fetch('http://localhost:54503/stat/averagepriceofgames')
    .then(x => x.json())
    .then(y => {
        averagepriceofgames = y;
        display();
    });
    await fetch('http://localhost:54503/stat/numberofgamespertype')
    .then(x => x.json())
    .then(y => {
        numberofgamespertype = y;
        display();
    });
}

function display() {
    document.getElementById('averagepriceofgameslabel').innerHTML = "";
    document.getElementById('averagepriceofgameslabel').innerHTML += "" + averagepriceofgames;

    document.getElementById('mostprofitablegamerevenue').innerHTML = "";
    document.getElementById('mostprofitablegamerevenue').innerHTML += "" + "x" + " with " + mostprofitablegame.totalRevenue;

    if (mostprofitablegame && mostprofitablegame.game) {
        document.getElementById('mostprofitablegamerevenue').innerHTML = "";
        document.getElementById('mostprofitablegamerevenue').innerHTML += "" + mostprofitablegame.game.title + " with " + mostprofitablegame.totalRevenue;
    }

    //console.log('******GAMES******')
    document.getElementById('resultarea').innerHTML = "";
    games.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.title + "</td><td>" + t.price + "</td><td>" + t.rating + "</td><td>" + t.copiesSold + "</td><td>" + t.price * t.copiesSold + "$</td><td>" + t.type + "</td><td>" + t.description + "</td><td>" + `<button type="button" onclick="remove(${t.id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.id})">Update</button>` + "</td></tr>"
        //console.log(t.title);
    });
    //console.log('******DEVELOPERS******')
    document.getElementById('resultareadevs').innerHTML = "";
    developers.forEach(t => {
        document.getElementById('resultareadevs').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.teamSize + "</td><td>" + `<button type="button" onclick="removedev(${t.id})">Delete</button>` + `<button type="button" onclick="showupdatedev(${t.id})">Update</button>` + "</td></tr>"
        //console.log(t.name);
    });

    //console.log('******PUBLISHERS******')
    document.getElementById('resultareapublishers').innerHTML = "";
    publishers.forEach(t => {
        document.getElementById('resultareapublishers').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + `<button type="button" onclick="removepub(${t.id})">Delete</button>` + `<button type="button" onclick="showupdatepub(${t.id})">Update</button>` + `<button type="button" onclick="showpubgames(${t.id})">List games</button>` + "</td></tr>"
        //console.log(t.name);
    });

    if (publisheridtoupdate>0) {
        

        document.getElementById('resultareaofpubs').innerHTML = "";
        console.log(gamesofpublisher);
        gamesofpublisher.forEach(t => {
            document.getElementById('resultareaofpubs').innerHTML +=
                "<tr><td>" + t.id + "</td><td>" + t.title + "</td><td>" + t.price + "</td><td>" + t.rating + "</td><td>" + t.copiesSold + "</td><td>" + t.price * t.copiesSold + "$</td><td>" + t.type + "</td><td>" + t.description + "</td></tr>"
        });
    }
}

function showpubgames(id) {
    document.getElementById('pubgames').style.display = 'flex';
    publisheridtoupdate = id;
    fetch('http://localhost:54503/stat/getgamesofpublisher/' + publisheridtoupdate)
        .then(x => x.json())
        .then(y => {
            gamesofpublisher = y;
        })
    display();
}


function showupdate(id) {
    document.getElementById('gametitletoupdate').value = games.find(t => t['id'] == id)['title'];
    document.getElementById('gamepricetoupdate').value = games.find(t => t['id'] == id)['price'];
    document.getElementById('gameratingtoupdate').value = games.find(t => t['id'] == id)['rating'];
    document.getElementById('gamecopiestoupdate').value = games.find(t => t['id'] == id)['copiesSold'];
    document.getElementById('gamedesctoupdate').value = games.find(t => t['id'] == id)['description'];
    document.getElementById('gamedevidtoupdate').value = games.find(t => t['id'] == id)['developerId'];
    document.getElementById('updateformdiv').style.display = 'flex';
    gameidtoupdate = id;
}

function showupdatedev(id) {
    document.getElementById('devnametoupdate').value = developers.find(t => t['id'] == id)['name'];
    document.getElementById('devteamsizetoupdate').value = developers.find(t => t['id'] == id)['teamSize'];
    document.getElementById('updateformdevdiv').style.display = 'flex';
    devidtoupdate = id;
}

function showupdatepub(id) {
    document.getElementById('publishernametoupdate').value = publishers.find(t => t['id'] == id)['name'];
    document.getElementById('updateformdivpub').style.display = 'flex';
    publisheridtoupdate = id;
}

function create() {
    let gametitle = document.getElementById('gametitle').value;
    fetch('http://localhost:54503/game', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            { title: gametitle }),})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => Console.error('Error: ', error));
}

function createdev() {
    let devname = document.getElementById('devname').value;
    fetch('http://localhost:54503/developer', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            { name: devname }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => Console.error('Error: ', error));
}

function createpub() {
    let pubname = document.getElementById('publishername').value;
    fetch('http://localhost:54503/publisher', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            { name: pubname }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => Console.error('Error: ', error));
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let gametitle = document.getElementById('gametitletoupdate').value;
    let gprice = document.getElementById('gamepricetoupdate').value;
    let grating = document.getElementById('gameratingtoupdate').value;
    let copis = document.getElementById('gamecopiestoupdate').value;
    let desc = document.getElementById('gamedesctoupdate').value;
    let devid = document.getElementById('gamedevidtoupdate').value;
    fetch('http://localhost:54503/game', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                title: gametitle, id: gameidtoupdate, price: gprice, rating: grating, copiesSold: copis, description: desc, developerId: devid
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => Console.error('Error: ', error));
}

function updatedev() {
    document.getElementById('updateformdevdiv').style.display = 'none';
    let devname = document.getElementById('devnametoupdate').value;
    let teamsize = document.getElementById('devteamsizetoupdate').value;
    fetch('http://localhost:54503/developer', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            { name: devname, id: devidtoupdate, teamSize: teamsize }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => Console.error('Error: ', error));
}

function updatepub() {
    document.getElementById('updateformdivpub').style.display = 'none';
    let pubname = document.getElementById('publishernametoupdate').value;
    fetch('http://localhost:54503/publisher', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            { name: pubname, id: publisheridtoupdate }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => Console.error('Error: ', error));
}

function remove(id) {
    fetch('http://localhost:54503/game/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error) });
}

function removedev(id) {
    fetch('http://localhost:54503/developer/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error) });
}

function removepub(id) {
    fetch('http://localhost:54503/publisher/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error) });
}