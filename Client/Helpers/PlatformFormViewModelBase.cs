using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Common;
using Prism.Commands;
using Prism.Events;

namespace Client.Helpers
{
    public abstract class PlatformFormViewModelBase : ViewModelBase
    {
        private bool _canSubmit;
        private RailwayPlatformModel _routeModel;

        #region ctors

        protected PlatformFormViewModelBase(ILogger logger, IEventAggregator eventAggregator = null) : this(logger, new RailwayPlatformModel(), eventAggregator)
        {
        }

        protected PlatformFormViewModelBase(ILogger logger, RailwayPlatformModel platform, IEventAggregator eventAggregator = null) : base(logger, eventAggregator)
        {
            InitEntranceTypes();

            RailwayPlatformModel = platform.Clone() as RailwayPlatformModel;
            RailwayPlatformModel.ErrorsChanged += RailwayPlatformModel_ErrorsChanged;

            SubmitCommand = new DelegateCommand(async () => await OnSubmitAsync());
        }

        #endregion ctors

        public bool CanSubmit
        {
            get { return _canSubmit; }
            set { SetProperty(ref _canSubmit, value); }
        }

        public ObservableCollection<EntranceType> EntranceTypes { get; set; }

        public RailwayPlatformModel RailwayPlatformModel
        {
            get { return _routeModel; }
            set { SetProperty(ref _routeModel, value); }
        }

        public ICommand SubmitCommand { get; set; }

        public override Task OnLoadedAsync()
        {
            UpdateCanSubmit();

            return Task.CompletedTask;
        }

        public abstract Task OnSubmitAsync();

        protected void UpdateCanSubmit()
        {
            CanSubmit = !RailwayPlatformModel.HasErrors;
        }

        private void InitEntranceTypes()
        {
            var entranceTypes = Enum.GetValues(typeof(EntranceType)).Cast<EntranceType>();
            EntranceTypes = new ObservableCollection<EntranceType>(entranceTypes);
        }

        private void RailwayPlatformModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            UpdateCanSubmit();
        }
    }
}