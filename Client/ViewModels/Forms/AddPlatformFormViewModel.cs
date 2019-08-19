using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class AddPlatformFormViewModel : PlatformFormViewModel
    {
        private readonly Action _onPlatformAdded;
        private readonly IRailwayPlatformService _platformService;

        public AddPlatformFormViewModel(
            IRailwayPlatformService platformService,
            ILogger logger,
            Action onPlatformAdded = null) : base(logger, new RailwayPlatformModel())
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