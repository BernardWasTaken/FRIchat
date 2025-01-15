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

async function deleteMessage(odgovorId) {
    try {
        const response = await fetch('/api/Api/delete', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({ odgovorId: odgovorId })
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Delete failed: ${response.status} - ${errorText || response.statusText}`);
        }
        
    } catch (error) {
        console.error("Error deleting message:", error);
        throw error;
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
    
    connection.on("DeleteForAll", function (odgovorId){
        console.log("deleted message:", odgovorId);
        
    });
    
    connection.on("ReceiveMessage", function (user, message, image, odgovorId) {
        console.log("ReceiveMessage message: " + message);
        console.log("ReceiveMessage user: " + user);
        console.log("ReceiveMessage image: " + image);
        var li = document.createElement("li");
        li.className = "komentar";
        var div = document.createElement("div");
        div.className = "glavaKomentarja";
        var pUsername = document.createElement("p");
        pUsername.className = "username";
        pUsername.innerHTML = user + " &nbsp;&nbsp;";
        var pDatum = document.createElement("p");
        pDatum.className = "datumObjave";
        var date = new Date();
        const month = new Intl.DateTimeFormat("en-US", { month: "2-digit", timeZone: "Europe/Ljubljana" }).format(date);
        const day = new Intl.DateTimeFormat("en-US", { day: "2-digit", timeZone: "Europe/Ljubljana" }).format(date);
        const time = new Intl.DateTimeFormat("en-US", { hour: "2-digit", minute: "2-digit", hour12: false, timeZone: "Europe/Ljubljana" }).format(date);
        pDatum.textContent = `${month}-${day} ${time}`;
        var flexDiv = document.createElement("div");
        flexDiv.style = "display: flex;";
        flexDiv.appendChild(pUsername);
        flexDiv.appendChild(pDatum);
        div.appendChild(flexDiv);
        
        if(user == document.getElementById("user").value){
            var del = document.createElement("div");
            del.className = "delete";
            del.id = "delete" + odgovorId;
            del.innerHTML = "";
            div.appendChild(del);
        }
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
    
    document.getElementById("delete").addEventListener("click", async function () {
        var odgovorId = document.getElementById("odgovorId").value;
        if (odgovorId !== "") {
            await deleteMessage(odgovorId);
            
            connection.invoke("DeleteMessage", odgovorId).catch(function(err) {
                return console.error(err);
            });
        }
    });

    document.getElementById("send-button").addEventListener("click", async function (event) {
        event.preventDefault();

        var user = document.getElementById("user").value;
        var message = document.getElementById("text-area").value;
        var vecja = 1000000;
        for (let i = 100000; i >= 0; i--) {
            if(document.getElementById("delete"+i)){
                vecja = i;
                break;
            }
        }
        //var odgovorId = document.getElementById("odgovorId"+vecja+1).value;
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