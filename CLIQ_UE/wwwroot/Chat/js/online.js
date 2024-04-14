var onlineUserHub = new signalR.HubConnectionBuilder().withUrl("/OnlineUsers").build();
onlineUserHub.start().then(function () {
    console.log("SignalR online User Hub connection established.");
});

onlineUserHub.on("Online", function (userId) {
    console.log("----------start online-----------");
    const otherUserid = document.querySelector('#otherUserid').textContent;
    if (otherUserid != null) {
        var elements = document.getElementsByClassName(userId);
        console.log(elements);
        var firstElement = elements[0];

        if (firstElement.classList.contains("online") == false) {

            firstElement.classList.add("online");
            firstElement.textContent = "Online";
        }
    }
    console.log("----------end online-----------");
});

onlineUserHub.on("OffLine", function (userId, timeFormated) {
    console.log("----------start OffLine-----------");
    const otherUserid = document.querySelector('#otherUserid').textContent;
    if (otherUserid != null) {
        var elements = document.getElementsByClassName(userId);
        console.log(elements);
        var firstElement = elements[0];

        if (firstElement.classList.contains("online")) {
            firstElement.classList.remove("online");
        }
        firstElement.textContent = timeFormated;
    }
    console.log("----------end OffLine-----------");
});