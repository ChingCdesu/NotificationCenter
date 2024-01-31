using NotificationCenter.Entities;
using NotificationCenter.Modals;

namespace NotificationCenter.Abstract;

public interface INotificationService
{
    public Task<bool> PushAsync(Device device, NotificationMessage message);
}