/*

Copyright (C) 2024 glomdom

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.

*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace Ploop.ViewModels;

public class NodeViewModel : ViewModelBase {
    private string _name;
    private bool _isHovered;
    private bool _isSelected;
    private double _x;
    private double _y;

    public ObservableCollection<PortViewModel> InputPorts { get; set; }
    public ObservableCollection<PortViewModel> OutputPorts { get; set; }

    public string Name {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public bool IsHovered {
        get => _isHovered;
        set => this.RaiseAndSetIfChanged(ref _isHovered, value);
    }

    public bool IsSelected {
        get => _isSelected;
        set => this.RaiseAndSetIfChanged(ref _isSelected, value);
    }
    
    public double X {
        get => _x;
        set => this.RaiseAndSetIfChanged(ref _x, value);
    }

    public double Y {
        get => _y;
        set => this.RaiseAndSetIfChanged(ref _y, value);
    }

    public NodeViewModel(string name, double x, double y, ObservableCollection<PortViewModel> inputPorts, ObservableCollection<PortViewModel> outputPorts) {
        _name = name;
        _isHovered = false;
        _isSelected = false;
        _x = x;
        _y = y;

        InputPorts = inputPorts;
        OutputPorts = outputPorts;
    }
}