using System.Collections.Generic;
using ReactiveUI;

namespace Ploop.ViewModels;

public class NodeViewModel : ReactiveObject {
    private double _x;
    public double X {
        get => _x;
        set => this.RaiseAndSetIfChanged(ref _x, value);
    }

    private double _y;
    public double Y {
        get => _y;
        set => this.RaiseAndSetIfChanged(ref _y, value);
    }

    public string Name { get; set; }

    public List<string> InputPorts { get; set; }
    public List<string> OutputPorts { get; set; }

    public NodeViewModel(string name, double x, double y) {
        Name = name;
        X = x;
        Y = y;

        InputPorts = new List<string>();
        OutputPorts = new List<string>();
    }
}