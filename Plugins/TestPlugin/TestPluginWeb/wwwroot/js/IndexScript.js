//Ajax
const ajaxbtn = document.getElementById("TestAjaxButton");
const ajaxOutput = document.getElementById("TestAjaxOutput");
const actionSelector = document.getElementById("ActionSelector");
//Circle
const circleContainer = document.getElementById("CircleContainer");
const circle = document.getElementById("Circle");
//Commands
const moveCircleBtn = document.getElementById("MoveCircle");
const inputX = document.getElementById("InputX");
const inputY = document.getElementById("InputY");
const circleCoordinatesOutput = document.getElementById("CircleCoordinates");
const commandConnection = new signalR.HubConnectionBuilder()
    .withUrl("/TestCommand")
    .configureLogging(signalR.LogLevel.Information)
    .build();
//Actions
const circleMoveRecording = document.getElementById("CircleMoveRecording");
const runActionBtn = document.getElementById("RunAction");
const actionOutput = document.getElementById("ActionOutput");
const actionConnection = new signalR.HubConnectionBuilder()
    .withUrl("/TestAction")
    .configureLogging(signalR.LogLevel.Information)
    .build();
//Scripts
const testScriptBtn = document.getElementById("TestScript");
const scriptInput = document.getElementById("ScriptInput");
const scriptOutput = document.getElementById("ScriptOutput");
const scriptConnection = new signalR.HubConnectionBuilder()
    .withUrl("/TestScript")
    .configureLogging(signalR.LogLevel.Information)
    .build();

//Ajax
ajaxbtn.onclick = async function () {

    const response = await fetch("/Test/api/TestAjax", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        ajaxOutput.innerHTML = await response.json();
    }
}

moveCircleBtn.onclick = async function () {
    commandConnection.invoke("MoveCircle", [inputX.value, inputY.value]);
};

//Actions
runActionBtn.onclick = async function (e) {
    actionConnection.invoke("Run", parseInt(actionSelector.value));
};
actionConnection.on("Add", action => {
    if (circleMoveRecording.checked) {
        let opt = document.createElement('option');
        opt.value = action.id;
        opt.innerHTML = action.name;
        actionSelector.appendChild(opt);
    }
});

actionConnection.on("Run", action => {
    actionOutput.innerHTML = action.name + " is runing";
});

//Scripts
scriptConnection.on("Script1Running", message => {
    scriptOutput.innerHTML = message;
});

testScriptBtn.onclick = async function () {
    scriptConnection.invoke("RunScript1", scriptInput.value);
};

//Cirle
var x = 0;
var y = 0;
var mousedown = false;
var commandBuffer = new Array();

circle.addEventListener('mousedown', function (e) {
    mousedown = true;
    x = circle.offsetLeft - e.clientX;
    y = circle.offsetTop - e.clientY;
}, true);

circle.addEventListener('mouseup', function (e) {
    mousedown = false;
    if (circleMoveRecording.checked) {
        actionConnection.invoke("Add", {
            "Id": parseInt(0),
            "Name": prompt("type action name"),
            "Repeat": parseInt(1),
            "Delay": parseInt(0),
            "Commands": commandBuffer
        });
        commandBuffer = new Array();
    }
}, true);

circleContainer.addEventListener('mousemove', function (e) {
    if (mousedown) {
        circle.style.left = e.clientX + x + 'px';
        circle.style.top = e.clientY + y + 'px';
        circleCoordinatesOutput.innerHTML = circle.style.left + " : " + circle.style.top;
        if (circleMoveRecording.checked) {
            commandBuffer.push({
                Delay: parseInt(17),
                Method: "MoveCircle",
                Parameters: [(e.clientX + x).toString(), (e.clientY + y).toString()]
            });
        }
    }
}, true);

commandConnection.on("MoveCircle", (varX, varY) => {
    circle.style.left = varX + "px";
    circle.style.top = varY + "px";
    circleCoordinatesOutput.innerHTML = circle.style.left + " : " + circle.style.top;
});

commandConnection.start();
actionConnection.start();
scriptConnection.start();
