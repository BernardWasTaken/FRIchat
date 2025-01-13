
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

window.onload = function() {
    scroolToBottom();
    addFileEventListener();
}