﻿// NClass - Free class diagram editor
// Copyright (C) 2020 Georgi Baychev
// 
// This program is free software; you can redistribute it and/or modify it under
// the terms of the GNU General Public License as published by the Free Software
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with
// this program; if not, write to the Free Software Foundation, Inc.,
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System.Collections.Generic;

namespace NClass.Core.UndoRedo
{
    public enum CommandId
    {
        AddShape,
        AddConnection,
        DeleteElements,
        ChangeProperty,
        MoveElements,
        Paste,
        AddMember,
        DeleteMember
    }

    public static class CommandIdToString
    {
        private static readonly Dictionary<CommandId, string> Strings = new Dictionary<CommandId, string>();

        static CommandIdToString()
        {
            Strings[CommandId.AddShape] = Translations.Strings.CommandAddShape;
            Strings[CommandId.AddConnection] = Translations.Strings.CommandAddConnection;
            Strings[CommandId.DeleteElements] = Translations.Strings.CommandDeleteElements;
            Strings[CommandId.ChangeProperty] = Translations.Strings.CommandChangeProperty;
            Strings[CommandId.MoveElements] = Translations.Strings.CommandMoveElements;
            Strings[CommandId.Paste] = Translations.Strings.CommandPaste;
            Strings[CommandId.AddMember] = Translations.Strings.CommandAddMember;
            Strings[CommandId.DeleteMember] = Translations.Strings.CommandDeleteMember;
        }

        public static string GetString(CommandId commandId)
        {
            switch (commandId)
            {
                case CommandId.AddShape:
                    return Strings[CommandId.AddShape];
                case CommandId.AddConnection:
                    return Strings[CommandId.AddConnection];
                case CommandId.DeleteElements:
                    return Strings[CommandId.DeleteElements];
                case CommandId.ChangeProperty:
                    return Strings[CommandId.ChangeProperty];
                case CommandId.MoveElements:
                    return Strings[CommandId.MoveElements];
                case CommandId.Paste:
                    return Strings[CommandId.Paste];
                case CommandId.AddMember:
                    return Strings[CommandId.AddMember];
                case CommandId.DeleteMember:
                    return Strings[CommandId.DeleteMember];
                default:
                    return Translations.Strings.Unknown;
            }
        }
    }
}