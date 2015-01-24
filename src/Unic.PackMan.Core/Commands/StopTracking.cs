﻿namespace Unic.PackMan.Core.Commands
{
    using Configuration;
    using Sitecore.Pipelines;
    using Sitecore.Shell.Framework.Commands;
    using Unic.PackMan.Core.Tracking;
    using Unic.PackMan.Core.User;

    /// <summary>
    /// Command to stop a tracking session
    /// </summary>
    public class StopTracking : Command
    {
        /// <summary>
        /// The tracking service
        /// </summary>
        private readonly ITrackingService trackingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking"/> class.
        /// HINT: Use poor man dependency injection here, because the Sitecore factory does only seem to work for
        /// events and pipelines. Dear Sitecore devs, why??
        /// </summary>
        public StopTracking() : this(new TrackingService(new UserService(), new ConfigurationService()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopTracking" /> class.
        /// </summary>
        /// <param name="trackingService">The tracking service.</param>
        public StopTracking(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }
        
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            CorePipeline.Run("PackMan.StopTracking", new PipelineArgs());
            Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
        }

        /// <summary>
        /// Get the state of the current command
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>State of the current command</returns>
        public override CommandState QueryState(CommandContext context)
        {
            if (!this.trackingService.IsTrackingEnabled())
            {
                return CommandState.Disabled;
            }

            return base.QueryState(context);
        }
    }
}
