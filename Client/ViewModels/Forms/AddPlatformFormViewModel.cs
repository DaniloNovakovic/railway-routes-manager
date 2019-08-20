using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Events;

namespace Client.ViewModels
{
    public class AddPlatformFormViewModel : PlatformFormViewModelBase
    {
        private readonly Action _onPlatformAdded;
        private readonly IRailwayPlatformService _platformService;

        public AddPlatformFormViewModel(
            IRailwayPlatformService platformService,
            ILogger logger,
            Action onPlatformAdded = null,
            IEventAggregator eventAggregator = null) : base(logger, new RailwayPlatformModel(), eventAggregator)
        {
            _platformService = platformService;
            _onPlatformAdded = onPlatformAdded;
        }

        public AddPlatformFormViewModel(
            IRailwayPlatformService platformService,
            ILogger logger,
            RailwayPlatformModel platformModel,
            Action onPlatformAdded = null,
            IEventAggregator eventAggregator = null) : base(logger, platformModel, eventAggregator)
        {
            _platformService = platformService;
            _onPlatformAdded = onPlatformAdded;
        }

        public override Task OnSubmitAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanSubmit = false;
                    await _platformService.AddPlatformAsync(RailwayPlatformModel);
                    OnRouteAdded();
                },
                @finally: UpdateCanSubmit);
        }

        private void OnRouteAdded()
        {
            _onPlatformAdded?.Invoke();
        }
    }
}