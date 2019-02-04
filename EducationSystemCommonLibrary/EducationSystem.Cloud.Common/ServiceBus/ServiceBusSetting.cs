namespace EducationSystem.Cloud.Common.ServiceBus
{
    public sealed class ServiceBusSetting
    {
        public string ConnectionString { get; private set; }

        public ServiceBusSetting(string connectionStrng)
        {
            ConnectionString = connectionStrng;
        }
    }
}
