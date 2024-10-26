using System.ComponentModel;
using MvvmDialogs.DialogTypeLocators;
using RAY.Library;

namespace NKnife.Module.UI.MainWorkbench.Internal;

public class DialogTypeLocator : IDialogTypeLocator
{
    private readonly Dictionary<string, Type> _vmViewDictionary = new Dictionary<string, Type>();

    public void Register(VmPair vmPair)
    {
        _vmViewDictionary.Add(vmPair.ViewModel.Name, vmPair.View);
    }
    
    public Type Locate(INotifyPropertyChanged viewModel)
    {
        var vmName = viewModel.GetType().Name;

        if(_vmViewDictionary.TryGetValue(vmName, out var locate))
        {
            return locate;
        }
        else
        {
            throw new NotImplementedException(vmName);
        }

        /*switch (vmName)
        {
            case nameof(ExpExplorerDialogVm):
                return typeof(ExperimentExplorerDialog);
            case nameof(ExpRunTimeDialogVm):
                return typeof(ExpRunTimeDialog);
            case nameof(CountDownDialogVm):
                return typeof(CountDownDialog);
            case nameof(CalibrationPositionDialogVm):
                return typeof(CalibrationPositionDialog);
            case nameof(CalibrationPlatePositionDialogVm):
                return typeof(CalibrationPlatePositionDialog);
            case nameof(OptionsDialogVm):
                return typeof(OptionsDialog);
            case nameof(CustomPipetteTechniqueDialogVm):
                return typeof(CustomPipetteTechniqueDialog);
            case nameof(LoggerDetailDialogViewModel):
                return typeof(LoggerDetailDialog);
            case nameof(PipetteTechniqueTestDialogVm):
                return typeof(PipetteTechniqueTestDialog);
            case nameof(ImportLiquidTypeDialogVm):
                return typeof(ImportLiquidTypeDialog);
            case nameof(ProgressBarDialogVm):
                return typeof(ProgressBarDialog);
            case nameof(EditTipboxDialogViewModel):
                return typeof(EditTipboxDialog);
            case nameof(EditPlateDialogViewModel):
                return typeof(EditPlateDialog);
            case nameof(EditTubeDialogViewModel):
                return typeof(EditTubeDialog);
            case nameof(EditReservoirDialogViewModel):
                return typeof(EditReservoirDialog);
            case nameof(EditAdapterDialogViewModel):
                return typeof(EditAdapterDialog);
            case nameof(VersionConflictInquiryDialogVm):
                return typeof(VersionConflictInquiryDialog);
            case nameof(SetPipetteComponentAliasDialogVm):
                return typeof(SetPipetteComponentAliasDialog);
            case nameof(ModifyXFreeDialogVm):
                return typeof(ModifyXFreeDialog);
            case nameof(OperatePipetteComponentDialogVm):
                return typeof(OperatePipetteComponentDialog);
            case nameof(SelectFrontArmDialogVm):
                return typeof(SelectFrontArmDialog);
            case nameof(CalibrationButtonDialogVm):
                return typeof(CalibrationButtonDialog);
            case nameof(CalibrationCheckDialogVm):
                return typeof(CalibrationCheckDialog);
            case nameof(CalibrationPipetteComponentDialogVm):
                return typeof(CalibrationPipetteComponentDialog);
            case nameof(BeforeFrontArmCalibrationDialogVm):
                return typeof(BeforeFrontArmCalibrationDialog);
            case nameof(FrontArmCalibrationCheckDialogVm):
                return typeof(FrontArmCalibrationCheckDialog);
            case nameof(FrontArmCalibrationDialogVm):
                return typeof(FrontArmCalibrationDialog);
            case nameof(SelectAvoidObstaclePolicyDialogVm):
                return typeof(SelectAvoidObstaclePolicyDialog);
            case nameof(DealWithGripperDialogVm):
                return typeof(DealWithGripperDialog);
            case nameof(DealWithPodDialogVm):
                return typeof(DealWithPodDialog);

            default:
                throw new NotImplementedException(vmName);
        }*/
    }
}
