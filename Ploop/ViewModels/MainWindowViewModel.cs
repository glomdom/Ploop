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