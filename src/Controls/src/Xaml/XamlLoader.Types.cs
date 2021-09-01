#nullable disable
//
// XamlLoader.cs
//
// Author:
//       Stephane Delcroix <stephane@mi8.be>
//
// Copyright (c) 2013 Mobile Inception
// Copyright (c) 2013-2014 Microsoft.Maui.Controls, Inc
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Xml;

namespace Microsoft.Maui.Controls.Xaml
{
	static partial class XamlLoader
	{
		public class RuntimeRootNode : RootNode
		{
			public RuntimeRootNode(XmlType xmlType, object root, IXmlNamespaceResolver resolver) : base(xmlType, resolver)
			{
				Root = root;
			}

			public object Root { get; internal set; }
		}

		public struct FallbackTypeInfo
		{
			public string ClrNamespace { get; internal set; }
			public string TypeName { get; internal set; }
			public string AssemblyName { get; internal set; }
			public string XmlNamespace { get; internal set; }
		}

		public struct CallbackTypeInfo
		{
			public string XmlNamespace { get; internal set; }
			public string XmlTypeName { get; internal set; }

		}
	}
}