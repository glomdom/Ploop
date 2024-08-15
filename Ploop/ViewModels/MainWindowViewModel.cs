using System;
using System.Collections.ObjectModel;

namespace Ploop.ViewModels;

public class MainWindowViewModel : ViewModelBase {
    public ObservableCollection<NodeViewModel> Nodes { get; } = [];
    public ObservableCollection<ConnectionViewModel> Connections { get; } = [];

    public MainWindowViewModel() {
        var testNode = new NodeViewModel(
            name: "Test Node",
            x: 100,
            y: 100
        );

        testNode.InputPorts.Add("I1");
        testNode.InputPorts.Add("I2");
        testNode.OutputPorts.Add("O1");

        Nodes.Add(testNode);
        
        foreach (var node in Nodes) {
            Console.WriteLine($"Node: {node.Name}, X: {node.X}, Y: {node.Y}");
        }
    }
}