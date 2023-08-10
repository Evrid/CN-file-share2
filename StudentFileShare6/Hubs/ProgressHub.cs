namespace StudentFileShare6.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ProgressHub : Hub
    {
        public async Task UpdateFileProgress(string fileID, int progressPercentage)
        //the UpdateFileProgressHub method is not explicitly called since it's intended to be invoked by the SignalR hub clients.
        {
            await Clients.All.SendAsync("UpdateFileProgressIdentifier", fileID, progressPercentage);
            //to edit to send to a specific client
            // "UpdateFileProgressIdentifier" is used as the event name or event identifier.
        }
    }

}

