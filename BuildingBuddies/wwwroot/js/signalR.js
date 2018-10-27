$(document).ready(function () {

    var messageInput = document.getElementById('message');

    messageInput.focus();

    var connection = new signalR.HubConnectionBuilder()
        .withUrl('/chat')
        .build();

    // nešto što se događa na broadcast
    connection.on('BroadcastMessage', function (name, message, direction) {
        console.log("Broadcast " + name + ": " + message);
        var encodedMsg = message;

        var liElement = document.createElement('li');
        liElement.setAttribute('class', 'message ' + direction + ' appeared');

        var divAvatar = document.createElement('div');
        divAvatar.setAttribute('class', 'avatar');

        var divTextWrapper = document.createElement('div');
        divTextWrapper.setAttribute('class', 'text_wrapper');

        var divText = document.createElement('div');
        divText.setAttribute('class', 'text');
        divText.innerHTML = encodedMsg;

        divTextWrapper.appendChild(divText);
        liElement.appendChild(divAvatar);
        liElement.appendChild(divTextWrapper);

        document.getElementById('discussion').appendChild(liElement);

        var divElem = document.getElementById('discussion');
        divElem.scrollTop = divElem.scrollHeight;
    });

    // primanje poruke i prikaz korisniku
    connection.on('Send', function (message) {
        console.log("Send: " + message);
        var encodedMsg = message;

        console.log('send funkcija');
        var liElement = document.createElement('li');
        liElement.innerHTML = encodedMsg;
        document.getElementById('discussion').appendChild(liElement);
    });

    // poziva se na onconnected
    // treba ispisati poruke (prima ih kao neku listu)
    connection.on('History', function (messages) {
        console.log('History');
        console.log(messages);

        // prije toga briše postojeće za svaki slučaj
        var myNode = document.getElementById('discussion');
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }

        messages.forEach(function (message) {

            var liElement = document.createElement('li');
            liElement.setAttribute('class', 'message ' + message.direction + ' appeared');

            var divAvatar = document.createElement('div');
            divAvatar.setAttribute('class', 'avatar');

            var divTextWrapper = document.createElement('div');
            divTextWrapper.setAttribute('class', 'text_wrapper');

            var divText = document.createElement('div');
            divText.setAttribute('class', 'text');
            divText.innerHTML = message.message;

            divTextWrapper.appendChild(divText);
            liElement.appendChild(divAvatar);
            liElement.appendChild(divTextWrapper);

            document.getElementById('discussion').appendChild(liElement);
        });

        var divElem = document.getElementById('discussion');
        divElem.scrollTop = divElem.scrollHeight;
    });

    // stavlja se event na slanje i dohvaća povijest poruka
    connection.start()
        .then(function () {
            console.log('connection started');
            document.getElementById('sendmessage').addEventListener('click', function (event) {
                console.log('sending... ');
                console.log(messageInput.value);
                connection.invoke('SendMessage', messageInput.value);

                // Clear text box and reset focus for next comment.
                messageInput.value = '';
                messageInput.focus();
                event.preventDefault();
            });
        })
        .catch(error => {
            console.error(error.message);
        });
});