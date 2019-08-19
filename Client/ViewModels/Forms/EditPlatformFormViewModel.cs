using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class EditPlatformFormViewModel : PlatformFormViewModel
    {
        private readonly Action _onPlatformAdded;
        private readonly IRailwayPlatformService _platformService;

        public EditPlatformFormViewModel(
            IRailwayPlatformService platformService,
            ILogger logger,
            RailwayPlatformModel platformModel,
            Action onPlatformAdded = null) : base(logger, platformModel)
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
                    await _platformService.UpdatePlatformAsync(RailwayPlatformModel);
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