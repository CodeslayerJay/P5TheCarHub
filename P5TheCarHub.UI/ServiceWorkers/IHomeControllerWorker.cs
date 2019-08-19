using P5TheCarHub.UI.Models.ViewModels;

namespace P5TheCarHub.UI.ServiceWorkers
{
    public interface IHomeControllerWorker
    {
        ContactFormModel ExecuteContact(int? vehicleId);
        HomeViewModel ExecuteIndex();
    }
}