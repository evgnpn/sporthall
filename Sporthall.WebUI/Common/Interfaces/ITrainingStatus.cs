namespace Sporthall.WebUI
{
    public interface ITrainingStatus
    {
        TrainingStatus TrainingStatus { get; set; }
        TrainingSubscribeStatus TrainingSubscribeStatus { get; set; }
    }
}
