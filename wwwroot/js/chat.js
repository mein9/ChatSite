const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", function (user, message) {
    const msg = `${user}: ${message}`;
    const li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    console.error(err.toString());
});

function sendMessage() {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    if (message.trim() !== "") {
        console.log("Sending message:", message);
        connection.invoke("SendMessage", message).catch(function (err) {
            console.error(err.toString());
        });
        document.getElementById("messageInput").value = '';
    } else {
        console.log("Empty message not sent.");
    }
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    sendMessage();
    event.preventDefault();
});