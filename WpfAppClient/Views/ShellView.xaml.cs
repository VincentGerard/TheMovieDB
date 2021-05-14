using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppClient.Views
{
	/// <summary>
	/// Interaction logic for ShellView.xaml
	/// </summary>
	public partial class ShellView : Window
	{
		public ShellView()
		{
			InitializeComponent();
			this.SourceInitialized += Window_SourceInitialized;
			Trace.WriteLine("[ShellView]Debut");
		}

		#region ResizeMode
		//https://stackoverflow.com/questions/386484/how-can-i-only-allow-uniform-resizing-in-a-wpf-window/696266#696266
		private static double _aspectRatio = 1.7777; //16/9
		private bool? _adjustingHeight = null;

		[StructLayout(LayoutKind.Sequential)]
		internal struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hwndInsertAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public int flags;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetCursorPos(ref Win32Point pt);

		[StructLayout(LayoutKind.Sequential)]
		internal struct Win32Point
		{
			public Int32 X;
			public Int32 Y;
		};

		internal enum SWP
		{
			NOMOVE = 0x0002
		}
		internal enum WM
		{
			WINDOWPOSCHANGING = 0x0046,
			EXITSIZEMOVE = 0x0232,
		}

		public static Point GetMousePosition() // mouse position relative to screen
		{
			Win32Point w32Mouse = new Win32Point();
			GetCursorPos(ref w32Mouse);
			return new Point(w32Mouse.X, w32Mouse.Y);
		}


		private void Window_SourceInitialized(object sender, EventArgs ea)
		{
			HwndSource hwndSource = (HwndSource)HwndSource.FromVisual((Window)sender);
			hwndSource.AddHook(DragHook);

			_aspectRatio = this.Width / this.Height;
		}

		private IntPtr DragHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			switch ((WM)msg)
			{
				case WM.WINDOWPOSCHANGING:
					{
						WINDOWPOS pos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));

						if ((pos.flags & (int)SWP.NOMOVE) != 0)
							return IntPtr.Zero;

						Window wnd = (Window)HwndSource.FromHwnd(hwnd).RootVisual;
						if (wnd == null)
							return IntPtr.Zero;

						// determine what dimension is changed by detecting the mouse position relative to the 
						// window bounds. if gripped in the corner, either will work.
						if (!_adjustingHeight.HasValue)
						{
							Point p = GetMousePosition();

							double diffWidth = Math.Min(Math.Abs(p.X - pos.x), Math.Abs(p.X - pos.x - pos.cx));
							double diffHeight = Math.Min(Math.Abs(p.Y - pos.y), Math.Abs(p.Y - pos.y - pos.cy));

							_adjustingHeight = diffHeight > diffWidth;
						}

						if (_adjustingHeight.Value)
							pos.cy = (int)(pos.cx / _aspectRatio); // adjusting height to width change
						else
							pos.cx = (int)(pos.cy * _aspectRatio); // adjusting width to heigth change

						Marshal.StructureToPtr(pos, lParam, true);
						handled = true;
					}
					break;
				case WM.EXITSIZEMOVE:
					_adjustingHeight = null; // reset adjustment dimension and detect again next time window is resized
					break;
			}
			return IntPtr.Zero;
		}



		#endregion

		
	}


}
