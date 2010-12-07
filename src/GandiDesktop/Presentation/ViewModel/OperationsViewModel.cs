using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;
using GandiDesktop.Gandi.Services.Hosting;

namespace GandiDesktop.Presentation.ViewModel
{
    public class OperationsViewModel : ViewModelBase
    {
        private const string ZeroOperation = "No Operation Pending";
        private const string TitleTemplate = "{0} Operations Pending";

        public int ProgressBarMin { get; private set; }
        public int ProgressBarMax { get; private set; }

        private int progressBarValue;
        public int ProgressBarValue
        {
            get { return this.progressBarValue; }
            set
            {
                if (this.progressBarValue != value)
                {
                    this.progressBarValue = value;
                    base.OnPropertyChanged(() => ProgressBarValue);
                }
            }
        }

        private string operationAction;
        public string OperationAction
        {
            get { return this.operationAction; }
            set
            {
                if (this.operationAction != value)
                {
                    this.operationAction = value;
                    base.OnPropertyChanged(() => OperationAction);
                }
            }
        }

        private string operationActionDirection;
        public string OperationActionDirection
        {
            get { return this.operationActionDirection; }
            set
            {
                if (this.operationActionDirection != value)
                {
                    this.operationActionDirection = value;
                    base.OnPropertyChanged(() => OperationActionDirection);
                }
            }
        }

        private string operationResourceName1;
        public string OperationResourceName1
        {
            get { return this.operationResourceName1; }
            set
            {
                if (this.operationResourceName1 != value)
                {
                    this.operationResourceName1 = value;
                    base.OnPropertyChanged(() => OperationResourceName1);
                }
            }
        }

        private OperationType operationResourceType1;
        public OperationType OperationResourceType1
        {
            get { return this.operationResourceType1; }
            set
            {
                if (this.operationResourceType1 != value)
                {
                    this.operationResourceType1 = value;
                    base.OnPropertyChanged(() => OperationResourceType1);
                }
            }
        }

        private string operationResourceName2;
        public string OperationResourceName2
        {
            get { return this.operationResourceName2; }
            set
            {
                if (this.operationResourceName2 != value)
                {
                    this.operationResourceName2 = value;
                    base.OnPropertyChanged(() => OperationResourceName2);
                }
            }
        }

        private OperationType operationResourceType2;
        public OperationType OperationResourceType2
        {
            get { return this.operationResourceType2; }
            set
            {
                if (this.operationResourceType2 != value)
                {
                    this.operationResourceType2 = value;
                    base.OnPropertyChanged(() => OperationResourceType2);
                }
            }
        }

        private bool operationHasResource2;
        public bool OperationHasResource2
        {
            get { return this.operationHasResource2; }
            set
            {
                if (this.operationHasResource2 != value)
                {
                    this.operationHasResource2 = value;
                    base.OnPropertyChanged(() => OperationHasResource2);
                }
            }
        }

        private int operationCount;
        public int OperationCount
        {
            get { return this.operationCount; }
            set
            {
                if (this.operationCount != value)
                {
                    this.operationCount = value;
                    base.OnPropertyChanged(() => OperationCount);
                }
            }
        }

        private bool hasOperations;
        public bool HasOperations
        {
            get { return this.hasOperations; }
            set
            {
                if (this.hasOperations != value)
                {
                    this.hasOperations = value;
                    base.OnPropertyChanged(() => HasOperations);
                }
            }
        }

        private bool hasOneOperation;
        public bool HasOneOperation
        {
            get { return this.hasOneOperation; }
            set
            {
                if (this.hasOneOperation != value)
                {
                    this.hasOneOperation = value;
                    base.OnPropertyChanged(() => HasOneOperation);
                }
            }
        }

        private bool collectionChanged;
        public bool CollectionChanged
        {
            get { return this.collectionChanged; }
            set
            {
                if (this.collectionChanged != value)
                {
                    this.collectionChanged = value;
                    base.OnPropertyChanged(() => CollectionChanged);
                }
            }
        }

        public ObservableCollection<OperationViewModel> OperationViewModelCollection { get; private set; }
        public ObservableCollection<OperationViewModel> CompletedOperationViewModelCollection { get; private set; }

        public OperationsViewModel()
        {
            this.UpdateTitle();

            DispatcherTimer refreshTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(15)
            };

            this.CompletedOperationViewModelCollection = new ObservableCollection<OperationViewModel>();
            this.OperationViewModelCollection = new ObservableCollection<OperationViewModel>();

            this.OperationViewModelCollection.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && (e.OldItems == null || e.OldItems.Count == 0))
                    this.ProgressBarValue = this.ProgressBarMax;

                this.OperationCount = this.OperationViewModelCollection.Count;
                this.HasOperations = this.OperationCount > 0;
                this.HasOneOperation = this.OperationCount == 1;

                this.CollectionChanged = true;
                this.CollectionChanged = false;

                this.UpdateTitle();

                refreshTimer.IsEnabled = this.HasOperations;
            };

            this.ProgressBarMin = 0;
            this.ProgressBarMax = 1000;
            this.ProgressBarValue = this.ProgressBarMin;

            refreshTimer.Tick += delegate
            {
                this.ProgressBarValue--;

                if (this.ProgressBarValue == this.ProgressBarMin)
                {
                    List<OperationViewModel> completedOperations = new List<OperationViewModel>();
                    foreach (OperationViewModel operationViewModel in this.OperationViewModelCollection)
                        if (operationViewModel.RefreshStatus() == OperationStep.Done)
                            completedOperations.Add(operationViewModel);
                    foreach (OperationViewModel operationViewModel in completedOperations)
                    {
                        this.CompletedOperationViewModelCollection.Add(operationViewModel);
                        this.OperationViewModelCollection.Remove(operationViewModel);
                    }

                    if (this.HasOperations)
                    {
                        this.ProgressBarValue = this.ProgressBarMax;
                    }

                    this.UpdateTitle();
                }
            };
        }

        private void UpdateTitle()
        {
            this.OperationActionDirection = null;
            this.OperationResourceName1 = null;
            this.OperationResourceName2 = null;
            this.OperationResourceType1 = OperationType.None;
            this.OperationResourceType2 = OperationType.None;
            
            if (this.OperationCount == 0)
            {
                this.OperationAction = OperationsViewModel.ZeroOperation;
            }
            else if (this.OperationCount == 1)
            {
                OperationViewModel operationViewModel = this.OperationViewModelCollection[0];

                this.OperationAction = operationViewModel.Action;
                this.OperationActionDirection = operationViewModel.ActionDirection;
                this.OperationResourceName1 = operationViewModel.ResourceName1;
                this.OperationResourceName2 = operationViewModel.ResourceName2;
                this.OperationResourceType1 = operationViewModel.ResourceType1;
                this.OperationResourceType2 = operationViewModel.ResourceType2;
            }
            else
            {
                this.OperationAction = String.Format(OperationsViewModel.TitleTemplate, this.OperationCount);
            }

            this.OperationHasResource2 = !String.IsNullOrEmpty(this.OperationResourceName2);
        }
    }
}