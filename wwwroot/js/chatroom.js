"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();



function scroolToBottom() {
    const div = document.getElementsByClassName("komentarji")[0];
    div.scrollTop = div.scrollHeight;
}

function addFileEventListener(){
    const div = document.getElementById("file-img");
    const input = document.getElementById("image-input");
    div.addEventListener("click", function(event){
        input.click();
    })
}

async function saveMessage(predmetId, vsebina, datotekaUrl) {
    try {
        const response = await fetch('/api/Api/save', { // Correct URL (api/Chat/save)
            method: 'POST',
            headers: {
                'Content-Type': 'application/json', // Important: Set to application/json
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value // Anti-forgery token
            },
            body: JSON.stringify({ predmetId: predmetId, vsebina: vsebina, datotekaUrl: datotekaUrl })
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Save failed: ${response.status} - ${errorText || response.statusText}`);
        }

        //const data = await response.json();
        //return data;
    } catch (error) {
        console.error("Error saving message:", error);
        throw error; // Re-throw the error for handling by the caller
    }
}

async function saveImage(file){
    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await fetch('/api/Api/upload', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            //Improved error handling
            const errorText = await response.text(); //Try to get error message from server
            throw new Error(`Upload failed: ${response.status} - ${errorText || response.statusText}`);
        }

        const data = await response.json();
        return data.filePath;
    } catch (error) {
        console.error("Error uploading image:", error);
        throw error; //Re-throw the error for handling by the caller
    }
}

window.onload = function() {
    scroolToBottom();
    addFileEventListener();

    document.getElementById("send-button").disabled = true;

    connection.on("ReceiveMessage", function (user, message, image) {
        console.log("ReceiveMessage message: " + message);
        console.log("ReceiveMessage user: " + user);
        console.log("ReceiveMessage image: " + image);
        var li = document.createElement("li");
        li.className = "komentar";
        var div = document.createElement("div");
        div.className = "glavaKomentarja";
        var pUsername = document.createElement("p");
        pUsername.className = "username";
        pUsername.textContent = user;
        var pDatum = document.createElement("p");
        pDatum.className = "datumObjave";
        pDatum.textContent = new Date().toLocaleDateString("sl-SI", { month: "2-digit", day: "2-digit", hour: "2-digit", minute: "2-digit", timeZone: "Europe/Ljubljana" });
        div.appendChild(pUsername);
        div.appendChild(pDatum);
        var br = document.createElement("br");
        li.appendChild(div);
        li.appendChild(br);
        if(message !== ""){
            li.innerHTML += message;
        }
        console.log("pri js: " + image);
        if(image !== ""){
            var img = document.createElement("img");
            img.src = image;
            console.log("img.src: "+img.src);
            img.alt = "ni slike pri js";
            li.appendChild(document.createElement("br"));
            li.appendChild(img);
        }

        document.getElementById("parent-ul").appendChild(li);
        scroolToBottom();
    });

    connection.start().then(function() {
        document.getElementById("send-button").disabled = false;
    }).catch(function(err) {
        return console.error(err);
    });

    document.getElementById("send-button").addEventListener("click", async function (event) {
        event.preventDefault();

        var user = document.getElementById("user").value;
        var message = document.getElementById("text-area").value;
        var image;
        var imagePath;
        if (document.getElementById("image-input").files.length > 0) {
            image = document.getElementById("image-input").files[0];
            imagePath = await saveImage(image);
            console.log(imagePath);
        } else {
            imagePath = "";
        }
        if(message !== "" || imagePath !== ""){
            console.log("img path: " +imagePath);
            await saveMessage(document.getElementById("predmetId").value, message, imagePath);
        }

        if ((imagePath === "" && message === "") || user === "") {
            return;
        }

        connection.invoke("SendMessage", user, message, imagePath).catch(function (err) {
            return console.error(err);
        });
        
        document.getElementById("image-input").value = "";
        document.getElementById("text-area").value = "";
    });
}