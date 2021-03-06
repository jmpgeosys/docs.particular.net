namespace Core6.Pipeline
{
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Logging;

    class OnReceivePipelineCompleted
    {
        void FromEndpointConfig(EndpointConfiguration endpointConfiguration, ILog log)
        {
            #region ReceivePipelineCompletedSubscriptionFromEndpointConfig

            endpointConfiguration.Pipeline.OnReceivePipelineCompleted(
                subscription: completed =>
                {
                    var duration = completed.CompletedAt - completed.StartedAt;
                    log.Info($"Reveive pipeline completed. Message: {completed.ProcessedMessage.MessageId}, duration: {duration}");
                    return Task.CompletedTask;
                });

            #endregion
        }

        class FromFeature : Feature
        {
            static ILog log = LogManager.GetLogger(typeof(FromFeature));

            protected override void Setup(FeatureConfigurationContext context)
            {
                #region ReceivePipelineCompletedSubscriptionFromFeature

                var pipeline = context.Pipeline;
                pipeline.OnReceivePipelineCompleted(
                    subscription: completed =>
                    {
                        var duration = completed.CompletedAt - completed.StartedAt;
                        log.Info($"Reveive pipeline completed: Message: {completed.ProcessedMessage.MessageId}, duration: {duration}");
                        return Task.CompletedTask;
                    });

                #endregion
            }
        }
    }
}