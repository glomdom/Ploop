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