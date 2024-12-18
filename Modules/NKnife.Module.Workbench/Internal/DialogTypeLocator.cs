using System.ComponentModel;
using MvvmDialogs.DialogTypeLocators;
using RAY.Library;

namespace NKnife.Module.Workbench.Internal;

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
    }
}