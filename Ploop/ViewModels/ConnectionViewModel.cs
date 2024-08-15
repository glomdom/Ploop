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
using Avalonia;
using ReactiveUI;

namespace Ploop.ViewModels;

public class ConnectionViewModel : ViewModelBase {
    private NodeViewModel _startNode;
    private NodeViewModel _endNode;

    public NodeViewModel StartNode {
        get => _startNode;
        set {
            this.RaiseAndSetIfChanged(ref _startNode, value);
            this.RaisePropertyChanged(nameof(StartPoint));
        }
    }

    public NodeViewModel EndNode {
        get => _endNode;
        set {
            this.RaiseAndSetIfChanged(ref _endNode, value);
            this.RaisePropertyChanged(nameof(EndPoint));
        }
    }

    public Point StartPoint {
        get => new Point(StartNode.X, StartNode.Y);
    }

    public Point EndPoint {
        get => new Point(EndNode.X, EndNode.Y);
    }

    public ConnectionViewModel(NodeViewModel startNode, NodeViewModel endNode) {
        _startNode = startNode ?? throw new ArgumentNullException(nameof(startNode));
        _endNode = endNode ?? throw new ArgumentNullException(nameof(endNode));
    }
}