//==========================================================================================
//
//		OpenNETCF.Win32.Win32Window
//		Copyright (c) 2003-2005, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//==========================================================================================
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OpenNETCF.Win32
{
	/// <summary>
	/// A helper class for working with native windows.
	/// </summary>
	public class Win32Window
	{
		//native window handle
		IntPtr hWnd;

		private Win32Window()
		{
		}

		public Win32Window(IntPtr hWnd)
		{
			this.hWnd = hWnd;
		}

		public static Win32Window FromControl(Control c)
		{
			c.Capture = true;
			IntPtr hWnd = GetCapture();
			c.Capture = false;

			if ( hWnd == IntPtr.Zero )
				throw new Exception("Unable to capture window");
			return new Win32Window(hWnd);
		}

		static public implicit operator IntPtr(Win32Window wnd)
		{
			return wnd.hWnd;
		}

		/// <summary>
		/// Returns the native window handle.
		/// <para><b>New in v1.3</b></para>
		/// </summary>
		public IntPtr Handle
		{
			get
			{
				return hWnd;
			}
		}
		

		/// <summary>
		/// This function creates an overlapped, pop-up, or child window with an extended style; otherwise, this function is identical to the CreateWindow function.
		/// </summary>
		/// <param name="dwExStyle">Specifies the extended style of the window</param>
		/// <param name="lpClassName"></param>
		/// <param name="lpWindowName"></param>
		/// <param name="dwStyle">Specifies the style of the window being created.</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="nWidth"></param>
		/// <param name="nHeight"></param>
		/// <param name="hWndParent"></param>
		/// <param name="hMenu"></param>
		/// <param name="hInstance"></param>
		/// <param name="lpParam"></param>
		/// <returns></returns>
		[DllImport("coredll.dll", EntryPoint="CreateWindowExW", SetLastError=true)] 
		public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName,
			string lpWindowName,int dwStyle, int x, int y, int nWidth, int nHeight,
			IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		/// <summary>
		/// This function destroys the specified window.
		/// </summary>
		/// <param name="hWnd">Handle to the window to be destroyed.</param>
		/// <returns>Nonzero indicates success.</returns>
		[DllImport("coredll.dll", SetLastError=true)] 
		public static extern IntPtr DestroyWindow(IntPtr hWnd);

		/// <summary>
		/// This function retrieves the handle to the top-level window whose class name and window name match the specified strings.
		/// </summary>
		/// <param name="className">The class name.</param>
		/// <param name="wndName">The window name (the window's title). If this parameter is NULL, all window names match.</param>
		/// <returns>A handle to the window that has the specified class name and window name indicates success. NULL indicates failure.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr FindWindow(string className, string wndName);

		/// <summary>
		/// Retrieves the handle to a window that has the specified relationship to the specified window.
		/// </summary>
		/// <param name="hWnd">Handle to a window.</param>
		/// <param name="nCmd">Specifies the relationship between the specified window and the window whose handle is to be retrieved.</param>
		/// <returns>A window handle indicates success. NULL indicates that no window exists with the specified relationship to the specified window.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, GW nCmd);

		/// <summary>
		/// Retrieves the handle to a window that has the specified relationship to the Win32Window.
		/// </summary>
		/// <param name="nCmd">Specifies the relationship between the specified window and the window which is to be retrieved.</param>
		/// <returns></returns>
		public Win32Window GetWindow(GW nCmd)
		{
			IntPtr hWndNext = GetWindow(hWnd, nCmd);
			return new Win32Window(hWndNext);	
		}
		
		
		/// <summary>
		/// Text of the Win32Window
		/// </summary>
		public string Text
		{
			get
			{
				return GetWindowText(hWnd);
			}
		}

		#region Get Window Text
		/// <summary>
		/// Copies the text of the specified window's title bar or controls body.
		/// </summary>
		/// <returns>The window text.</returns>
		public static string GetWindowText(IntPtr hWnd)
		{
			StringBuilder sb = new StringBuilder(256);
			int len = GetWindowText(hWnd, sb, sb.Capacity);
			if ( len > 0 )
				sb.Length = len;
			return sb.ToString();
		}
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder sb, int maxCount);
		#endregion


		#region GetWindowRect

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern bool GetWindowRect(IntPtr hWnd, byte[] rect);
		
		/// <summary>
		/// Returns a Rectangle representing the bounds of the window
		/// </summary>
		/// <param name="hWnd">a valid window handle</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle"/> representing the bounds of the specified window.</returns>
		public static Rectangle GetWindowRect(IntPtr hWnd)
		{
			byte[] rect = new byte[16];

			if(!GetWindowRect(hWnd, rect))
			{
				throw new WinAPIException("Failed getting window rectangle");
			}

			int x = BitConverter.ToInt32(rect, 0);
			int y = BitConverter.ToInt32(rect, 4);
			int width = BitConverter.ToInt32(rect, 8) - x;
			int height = BitConverter.ToInt32(rect, 12) - y;
			Rectangle result = new Rectangle(x, y, width, height);

			return result;
		}
		#endregion

		/// <summary>
		/// Retrieves the window handle to the active window associated with the thread that calls the function.
		/// </summary>
		/// <returns>The handle to the active window associated with the calling thread's message queue indicates success. NULL indicates failure.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr GetActiveWindow();

		/// <summary>
		/// Retrieves the handle to the keyboard focus window associated with the thread that called the function.
		/// </summary>
		/// <returns>The handle to the window with the keyboard focus indicates success. NULL indicates that the calling thread's message queue does not have an associated window with the keyboard focus.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr GetFocus();

		/// <summary>
		/// Retrieves the handle to the window, if any, that has captured the mouse or stylus input. Only one window at a time can capture the mouse or stylus input.
		/// </summary>
		/// <returns>The handle of the capture window associated with the current thread indicates success. NULL indicates that no window in the current thread has captured the mouse.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr GetCapture();

		/// <summary>
		/// Retrieves information about the specified window.
		/// </summary>
		/// <param name="hWnd">Handle to the window and, indirectly, the class to which the window belongs.</param>
		/// <param name="nItem">Specifies the zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four; for example, if you specified 12 or more bytes of extra memory, a value of 8 would be an index to the third 32-bit integer.</param>
		/// <returns>The requested 32-bit value indicates success. Zero indicates failure.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nItem);

		/// <summary>
		/// This function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window's client area.
		/// </summary>
		/// <param name="hWnd">Handle to the window.</param>
		/// <param name="X">Specifies the new position of the left side of the window.</param>
		/// <param name="Y">Specifies the new position of the top of the window.</param>
		/// <param name="cx">Specifies the new width of the window.</param>
		/// <param name="cy">Specifies the new height of the window.</param>
		/// <returns>Nonzero indicates success. Zero indicates failure.</returns>
		[DllImport("coredll.dll", SetLastError=true)] 
		public static extern IntPtr MoveWindow(IntPtr hWnd, int X, int Y, int cx, int cy);

		/// <summary>
		/// Places a message in the message queue associated with the thread that created the specified window and then returns without waiting for the thread to process the message.
		/// </summary>
		/// <param name="hWnd">Handle to the window whose window procedure is to receive the message.</param>
		/// <param name="msg">Specifies the message to be posted.</param>
		/// <param name="wParam">Specifies additional message-specific information.</param>
		/// <param name="lParam">Specifies additional message-specific information.</param>
		/// <returns>Nonzero indicates success. Zero indicates failure.</returns>
		[DllImport("coredll.dll", SetLastError=true)] 
		public static extern IntPtr PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Changes an attribute of the specified window.
		/// </summary>
		/// <param name="hWnd">Handle to the window and, indirectly, the class to which the window belongs.</param>
		/// <param name="GetWindowLongParam">Specifies the zero-based offset to the value to be set.</param>
		/// <param name="nValue">Specifies the replacement value.</param>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern void SetWindowLong(IntPtr hWnd, int GetWindowLongParam, int nValue);
		
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, int lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, String lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, String lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, byte[] lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, byte[] lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern int DefWindowProc(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// enables or disables mouse and keyboard input to the specified window or control. When input is disabled, the window does not receive input such as mouse clicks and key presses. When input is enabled, the window receives all input.
		/// </summary>
		/// <param name="hWnd">Handle to the window to be enabled or disabled.</param>
		/// <param name="bEnable">Boolean that specifies whether to enable or disable the window. If this parameter is TRUE, the window is enabled. If the parameter is FALSE, the window is disabled.</param>
		/// <returns>Nonzero indicates that the window was previously disabled. Zero indicates that the window was not previously disabled.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern bool EnableWindow( IntPtr hWnd, bool bEnable );

		/// <summary>
		/// Defines a new window message that is guaranteed to be unique throughout the system. The returned message value can be used when calling the SendMessage or PostMessage function.
		/// </summary>
		/// <param name="lpMsg">String that specifies the message to be registered.</param>
		/// <returns>A message identifier in the range 0xC000 through 0xFFFF indicates that the message is successfully registered. Zero indicates failure.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern int RegisterWindowMessage(string lpMsg);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr CallWindowProc(IntPtr pfn, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("coredll.dll", SetLastError=true)]
		public static extern IntPtr CallWindowProc(IntPtr pfn, IntPtr hWnd, WM msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Sets the keyboard focus to the specified window. All subsequent keyboard input is directed to this window. The window, if any, that previously had the keyboard focus loses it.
		/// </summary>
		/// <param name="hWnd">Handle to the window that will receive the keyboard input.
		/// If this parameter is NULL, keystrokes are ignored.</param>
		/// <returns>The handle to the window that previously had the keyboard focus indicates success. NULL indicates that the hWnd parameter is invalid or the window is not associated with the calling thread's message queue.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern bool SetFocus(IntPtr hWnd);

		/// <summary>
		/// Puts the thread that created the specified window into the foreground and activates the window.
		/// </summary>
		/// <param name="hWnd">Handle to the window that should be activated and brought to the foreground.</param>
		/// <returns>Nonzero indicates that the window was brought to the foreground. Zero indicates that the window was not brought to the foreground.</returns>
		[DllImport("coredll.dll", SetLastError=true)]
		public static extern bool SetForegroundWindow(IntPtr hWnd ); 

		[DllImport("coredll.dll", SetLastError=true)]
		public extern static int SetWindowPos (
			IntPtr hWnd, HWND pos,
			int X, int Y, int cx, int cy, SWP uFlags);

		public static void UpdateWindowStyle(IntPtr hwnd, int RemoveStyle, int AddStyle) 
		{
			int style = GetWindowLong(hwnd, (int)GWL.STYLE);
			style &= ~RemoveStyle;
			style |= AddStyle;
			SetWindowLong(hwnd, (int)GWL.STYLE, style);
			SetWindowPos(hwnd, 0, 0, 0, 0, 0, SWP.NOMOVE |
				SWP.NOZORDER | SWP.NOSIZE |
				SWP.NOACTIVATE | SWP.FRAMECHANGED);
		}

		public static void SetWindowStyle(IntPtr hwnd, int style) 
		{
			SetWindowLong(hwnd, (int)GWL.STYLE, style);
			SetWindowPos(hwnd, 0, 0, 0, 0, 0, SWP.NOMOVE |
				SWP.NOZORDER | SWP.NOSIZE |
				SWP.NOACTIVATE | SWP.FRAMECHANGED);
		}

		/// <summary>
		/// This function changes the parent window of the specified child window.
		/// </summary>
		/// <param name="hWChild">Handle to the child window.</param>
		/// <param name="hwParent">Handle to the new parent window. If this parameter is NULL, the desktop window becomes the new parent window.</param>
		/// <returns>A handle to the previous parent window indicates success. NULL indicates failure.</returns>
		[DllImport("coredll.dll", EntryPoint="SetParent", SetLastError=true)] 
		public static extern IntPtr SetParent(IntPtr hWChild, IntPtr hwParent);

		[DllImport("commctrl.dll", SetLastError=true)]
		public static extern bool InitCommonControlsEx(byte[] data);

	}

	/// <summary>
	/// Notification header returned in WM_NOTIFY lParam
	/// </summary>
	public struct NMHDR
	{
		/// <summary>
		/// Window that has sent WM_NOTIFY
		/// </summary>
		public IntPtr  hwndFrom;
		/// <summary>
		/// Control ID of the window that sent the notification
		/// </summary>
		public int  idFrom;
		/// <summary>
		/// Notification code
		/// </summary>
		public int  code;         // NM_ code
	}


}

/* ============================== Change Log ==============================
 * ---------------------
 * Oct 20, Chris Tacke
 * - Added changes submitted by Chris Schletter
 *   - Added WS_VISIBLE
 *   - Added WM_SETTEXT
 *   - Added CreateWindow, SetWindowStyle
 * ---------------------
 *
 * ---------------------
 * Aug 22, Peter Foot
 * ---------------------
 * - Namespace changed to Win32
 * 
 * ---------------------
 * May 13, Alex Feinman
 * ---------------------
 * - Added CallWindowProc, GetCapture
 * - Added enum WindowMessage
 * ---------------------
 * ---------------------
 * May 12, Alex Yakhnin
 * ---------------------
 * - Added SetFocus, SetWindowPos and UpdateWindowStyle SetWindowStyle helper methods
 * ---------------------
 * ---------------------
 * May 9, Alex Feinman
 * ---------------------
 * - Added RegisterWindowMessage
 * ---------------------
 * ======================================================================== */
