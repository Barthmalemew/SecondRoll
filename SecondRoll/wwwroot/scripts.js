const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", function (user, message) {
    const messageList = document.getElementById("messages");
    const messageItem = document.createElement("li");
    messageItem.textContent = `${user}: ${message}`;
    messageList.appendChild(messageItem);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.querySelector('button').addEventListener('click', function () {
    const message = document.querySelector('input').value;
    const user = localStorage.getItem('username');
    if (message) {
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        document.querySelector('input').value = '';
    }
});
