/*
' Copyright (c) 2023 intelequia.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Framework;
using System;

namespace Intelequia.Modules.DNNPulse.Data
{
    public abstract class DataProvider
    {
        #region Shared/Statics Methods
        // Singleton reference to the instantiated object 
        private static DataProvider _objProvider;

        // Constructor
        static DataProvider()
        {
            CreateProvider();
        }

        // Create provider dynamically
        private static void CreateProvider()
        {
            // The Namespace Name
            _objProvider = (DataProvider)Reflection.CreateObject("data", "Intelequia.Modules.DNNPulse.Data", "");
        }

        public static DataProvider Instance()
        {
            return _objProvider;
        }
        #endregion
        #region DNNPulse


        public abstract Model.DNNPulse GetDNNPulse();

        #endregion
    }

}