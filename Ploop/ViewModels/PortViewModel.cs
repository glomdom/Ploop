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

using ReactiveUI;

namespace Ploop.ViewModels;

public class PortViewModel : ViewModelBase {
    public enum PortType {
        Input,
        Output
    }

    private string _name;
    private bool _isHovered;
    private bool _isSelected;
    private PortType _type;

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

    public PortType Type {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public PortViewModel(string name, PortType type) {
        _name = name;
        _type = type;
        _isHovered = false;
        _isSelected = false;
    }
}