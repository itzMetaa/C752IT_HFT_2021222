let games = [];
let connection = null;
let gameidtoupdate = -1;


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
        //console.log(games);
        display();
    });

}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    games.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.title + "</td><td>" + t.price + "</td><td>" + t.rating + "</td><td>" + t.copiesSold + "</td><td>" + t.type + "</td><td>" + t.description + "</td><td>" + `<button type="button" onclick="remove(${t.id})">Delete</button>` + `<button type="button" onclick="showupdate(${t.id})">Update</button>` +"</td></tr>"
        console.log(t.title);
    })
}

function showupdate(id) {
    document.getElementById('gametitletoupdate').value = games.find(t => t['id'] == id)['title'];
    document.getElementById('updateformdiv').style.display = 'flex';
    gameidtoupdate = id;
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

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let gametitle = document.getElementById('gametitletoupdate').value;
    fetch('http://localhost:54503/game', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            { title: gametitle, id: gameidtoupdate }),
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