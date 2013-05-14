' NuGardt SC2Ranks API
' Copyright (C) 2011-2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Namespace SC2Ranks.Helper
''' <summary>
'''   Attribute for Notation.
''' </summary>
  ''' <remarks></remarks>
  <DebuggerStepThrough()>
  <AttributeUsage(AttributeTargets.Field, AllowMultiple:=False, Inherited:=False)>
  Public Class NotationAttribute
    Inherits Attribute

    Public ReadOnly Value As String

    Public Sub New(ByVal Value As String)
      Me.Value = Value
    End Sub

    Public Overrides Function ToString() As String
      Return Me.Value
    End Function
  End Class
End Namespace