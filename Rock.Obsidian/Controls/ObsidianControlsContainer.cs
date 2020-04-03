// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;

namespace Rock.Obsidian.Controls
{
    public static class ObsidianControlsContainer
    {
        private static Dictionary<string, Type> _container = new Dictionary<string, Type>();

        /// <summary>
        /// Gets the specified control type.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <returns></returns>
        public static Type Get(string controlName)
        {
            var controlType = _container.GetValueOrNull( controlName );

            if (controlType != null)
            {
                return controlType;
            }

            var path = $"Rock.Obsidian.Controls.{controlName}";
            controlType = Type.GetType( path );

            if ( controlType != null )
            {
                _container[controlName] = controlType;
            }

            return controlType;
        }
    }
}
