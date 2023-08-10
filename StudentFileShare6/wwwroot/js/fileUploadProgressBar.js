



const connection = new signalR.HubConnectionBuilder()
  .withUrl('/progressHub')
  .build();

connection.start().then(() => {
  console.log("SignalR connection established.");
}).catch(err => {
  console.error("Error establishing SignalR connection:", err);
});



function updateProgressBar(progressBarId, progressTextId, Progress) {
 // console.log("test2");
  var progressBar1 = document.getElementById(progressBarId);
//  console.log("test3");
 // console.log("Received progressBar1:", progressBar1);
  var width = Progress;
  progressBar1.style.width = width + "%";



  var dynamicTextElement = document.getElementById(progressTextId);
  
  dynamicTextElement.innerHTML = Progress+" percent";

}



