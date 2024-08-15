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