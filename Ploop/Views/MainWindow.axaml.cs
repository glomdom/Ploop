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
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Ploop.ViewModels;
using ReactiveUI;

namespace Ploop.Views {
    public partial class MainWindow : Window {
        private NodeViewModel? _draggedNode;
        private double _offsetX;
        private double _offsetY;

        public MainWindow() {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            SetupNodes();

            PointerPressed += OnPointerPressed;
            PointerMoved += OnPointerMoved;
            PointerReleased += OnPointerReleased;

            SetupConnections();
        }

        private void SetupNodes() {
            var canvas = this.FindControl<Canvas>("Workspace");
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel == null || canvas == null)
                return;

            foreach (var node in viewModel.Nodes) {
                var border = CreateNodeElement(node);

                Canvas.SetLeft(border, node.X);
                Canvas.SetTop(border, node.Y);

                canvas.Children.Add(border);
            }
        }

        private Border CreateNodeElement(NodeViewModel node) {
            var header = new Border {
                Background = new SolidColorBrush(Color.Parse("#2C3E50")),
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                CornerRadius = new CornerRadius(8, 8, 0, 0),
                Child = new TextBlock {
                    Text = node.Name,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };

            var portsGrid = new Grid {
                ColumnDefinitions = new ColumnDefinitions("Auto,*,Auto"),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            int totalPorts = Math.Max(node.InputPorts.Count, node.OutputPorts.Count);

            for (int i = 0; i < totalPorts; i++) {
                if (i < node.InputPorts.Count) {
                    var inputPort = CreatePort(node.InputPorts[i]);
                    portsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                    portsGrid.Children.Add(inputPort);

                    Grid.SetRow(inputPort, i);
                    Grid.SetColumn(inputPort, 0);
                }

                if (i < node.OutputPorts.Count) {
                    var outputPort = CreatePort(node.OutputPorts[i]);
                    portsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                    portsGrid.Children.Add(outputPort);
                    
                    Grid.SetRow(outputPort, i);
                    Grid.SetColumn(outputPort, 2);
                }
            }

            var content = new Border {
                Background = new SolidColorBrush(Color.Parse("#34495E")),
                Padding = new Thickness(10),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                CornerRadius = new CornerRadius(0, 0, 8, 8),
                Child = portsGrid
            };

            var container = new StackPanel {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Children = {
                    header,
                    content
                }
            };

            var border = new Border {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(8),
                Tag = node,
                Child = container
            };

            return border;
        }

        private Border CreatePort(string label) {
            return new Border {
                Background = Brushes.Gray,
                CornerRadius = new CornerRadius(12),
                Width = 24,
                Height = 24,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Child = new TextBlock {
                    Text = label,
                    Foreground = Brushes.White,
                    FontSize = 12,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };
        }

        private void SetupConnections() {
            var canvas = this.FindControl<Canvas>("Workspace");
            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel == null || canvas == null)
                return;

            foreach (var connection in viewModel.Connections) {
                var line = new Line {
                    Stroke = Brushes.LightBlue,
                    StrokeThickness = 2,
                    StartPoint = connection.StartPoint,
                    EndPoint = connection.EndPoint
                };

                connection.WhenAnyValue(c => c.StartPoint, c => c.EndPoint)
                    .Subscribe(_ => {
                        line.StartPoint = connection.StartPoint;
                        line.EndPoint = connection.EndPoint;
                    });

                canvas.Children.Add(line);
            }
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e) {
            var canvas = this.FindControl<Canvas>("Workspace");
            if (canvas == null) return;

            var position = e.GetPosition(canvas);
            _draggedNode = null;

            foreach (var element in canvas.Children) {
                if (element is not Border { Tag: NodeViewModel node } border) continue;

                var nodePosition = new Point(Canvas.GetLeft(border), Canvas.GetTop(border));

                if (position.X >= nodePosition.X && position.X <= nodePosition.X + border.Bounds.Width &&
                    position.Y >= nodePosition.Y && position.Y <= nodePosition.Y + border.Bounds.Height) {
                    _draggedNode = node;
                    _offsetX = position.X - nodePosition.X;
                    _offsetY = position.Y - nodePosition.Y;
                    break;
                }
            }
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e) {
            if (_draggedNode == null) return;

            var canvas = this.FindControl<Canvas>("Workspace");
            if (canvas == null) return;

            var position = e.GetPosition(canvas);
            var newX = position.X - _offsetX;
            var newY = position.Y - _offsetY;

            _draggedNode.X = newX;
            _draggedNode.Y = newY;

            foreach (var element in canvas.Children) {
                if (element is Border border && border.Tag == _draggedNode) {
                    Canvas.SetLeft(border, newX);
                    Canvas.SetTop(border, newY);
                }
            }
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e) {
            _draggedNode = null;
        }
    }
}