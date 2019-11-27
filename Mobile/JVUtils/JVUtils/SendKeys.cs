//==========================================================================================
//
//		OpenNETCF.Windows.Forms.SendKeys
//		Copyright (c) 2004-2005, OpenNETCF.org
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
using System.Globalization;
using System.Runtime.InteropServices;

namespace JVUtils
{
	/// <summary>
	/// Provides methods for sending keystrokes to an application.
	/// </summary>
	/// <remarks>
	/// A quote from "http://msdn2.microsoft.com/library/k3w7761b.aspx":
	/// Each key is represented by one or more characters. To specify a single keyboard character, 
	/// use the character itself. For example, to represent the letter A, pass in the string "A" to 
	/// the method. To represent more than one character, append each additional character to the 
	/// one preceding it. To represent the letters A, B, and C, specify the parameter as "ABC".
	/// 
	/// The plus sign (+), caret (^), percent sign (%), tilde (~), and parentheses () have special 
	/// meanings to SendKeys. To specify one of these characters, enclose it within braces ({}). 
	/// For example, to specify the plus sign, use "{+}". To specify brace characters, use "{{}" 
	/// and "{}}". Brackets ([ ]) have no special meaning to SendKeys, but you must enclose them 
	/// in braces.
	/// 
	/// To specify that any combination of SHIFT (+), CTRL (^), and ALT (%) should be held down while several 
	/// other keys are pressed, enclose the code for those keys in parentheses. For example, to 
	/// specify to hold down SHIFT while E and C are pressed, use "+(EC)". To specify to hold down 
	/// SHIFT while E is pressed, followed by C without SHIFT, use "+EC".
	/// 
	/// To specify repeating keys, use the form {key number}. You must put a space between key 
	/// and number. For example, {LEFT 42} means press the LEFT ARROW key 42 times; {h 10} 
	/// means press H 10 times.
	/// </remarks>
	public class SendKeys
	{
		private SendKeys()
		{
		}

		private static int MatchKeyword(string keyword)
		{
			for (int i = 0; i < keywords.Length; i++)
			{
				if (String.Compare(keywords[i].keyword, keyword, true, CultureInfo.InvariantCulture) == 0)
				{
					return SendKeys.keywords[i].vk;
				}
			}

			return -1;
		}

		private static void CancelMods(byte [] keys, int level)
		{
            SetMods(keys, level, 0);			
		}

		private static void SetMods(byte [] keys, int level, byte value)
		{
			if (keys[SHIFTPOS] == level)
			{
				if (value == 0) SendKey(SHIFTVK, KEYEVENTF_KEYUP);
                keys[SHIFTPOS] = value;                				
			}
			if (keys[CONTROLPOS] == level)
			{
				if (value == 0) SendKey(CONTROLVK, KEYEVENTF_KEYUP);
                keys[CONTROLPOS] = value;
			}
			if (keys[ALTPOS] == level)
			{
				if (value == 0) SendKey(ALTVK, KEYEVENTF_KEYUP);
				keys[ALTPOS] = value;
			}
		}
 
		/// <summary>
		/// Sends keystrokes to the active application.
		/// </summary>
		/// <param name="keys">The string of keystrokes to send.</param>
		public static void Send(string keys)
		{
			byte [] mods = new byte[3];

			char ch;
			int pos;
			int index = 0;
			byte group = 0;
			while(index < keys.Length)
			{
				ch = keys[index];
				switch(ch)
				{
					case '(': // start group
						if (group > 3) throw new ArgumentException("Nesting error."); 
						group++;
						SetMods(mods, 4, group);
						break;

					case ')': // end group
						if (group < 1) throw new ArgumentException("'(' is missing."); 
						CancelMods(mods, group);
						group--;
						break;

					case '+':
						if (mods[SHIFTPOS] != 0) throw new ArgumentException("Invalid SendKey string."); 
						mods[SHIFTPOS] = 4;
						SendKey(SHIFTVK, KEYEVENTF_KEYDOWN);
						break;

					case '^':
						if (mods[CONTROLPOS] != 0) throw new ArgumentException("Invalid SendKey string."); 
						mods[CONTROLPOS] = 4;
						SendKey(CONTROLVK, KEYEVENTF_KEYDOWN);
						break;

					case '%':
						if (mods[ALTPOS] != 0) throw new ArgumentException("Invalid SendKey string."); 
						mods[ALTPOS] = 4;
						SendKey(ALTVK, KEYEVENTF_KEYDOWN);
						break;

					case '~': // ENTER key 
						SendChar(13, mods);
						break;

					case '{':
					{
						index++;
						if ((pos = keys.IndexOf('}', index)) < 0) throw new ArgumentException("'}' is missing.");
						string t = keys.Substring(index, pos - index);
						if (t.Length == 0) // {}}?
						{
							index++;
							if (keys.Length > index && keys[index] == '}')
								SendChar((byte)'}', mods);
							else
								throw new ArgumentException("'{}' is invalid sequence."); 
						}
						else 
						{
							int count;
							index += t.Length;
							string [] s = t.Split(new char[]{' '}); 
							if (s.Length > 2) throw new ArgumentException("Incorrect string in the {...}");
							if (s.Length == 2) 
							{
								t = s[0];
								count = Convert.ToInt32(s[1]);
							}
							else
								count = 1;

							int vk = MatchKeyword(t);
							if (vk < 0) // key has not been found
							{
								if (t.Length == 1) // is it {+}, {~}, {^}, etc.
									vk = (byte)t[0];
								else
									throw new ArgumentException("Keyword '" + t + "' has not been found.");
							}
							
							for(int i = 0; i < count; i++)
								SendChar((byte)vk, mods);
						}
						break;
					}

					default:
						SendChar((byte)ch, mods);
						break;

				}

				index++;
			}

			CancelMods(mods, 4);
		}

		private class KeywordVk
		{
			internal string keyword;
			internal int vk;

			public KeywordVk(string key, int v)
			{
				this.keyword = key;
				this.vk = v;
			}
		}

		private static void SendKey(byte k, int flags)
		{
			keybd_event(k, 0, flags, 0);
		}

		private static void SendChar(byte k, byte [] mods)
		{
			keybd_event(k, 0, 0, 0);
			keybd_event(k, 0, KEYEVENTF_KEYUP, 0);

			CancelMods(mods, 4);
		}

        private static KeywordVk[] keywords = new KeywordVk [] 
		{ 
			new KeywordVk("ENTER", 13), new KeywordVk("TAB", 9), 
			new KeywordVk("ESC", 0x1b), new KeywordVk("ESCAPE", 0x1b), 

			new KeywordVk("HOME", 0x24), new KeywordVk("END", 0x23), 
			new KeywordVk("LEFT", 0x25), new KeywordVk("RIGHT", 0x27), 
			new KeywordVk("UP", 0x26), new KeywordVk("DOWN", 40), 
			new KeywordVk("PGUP", 0x21), new KeywordVk("PGDN", 0x22), 
			new KeywordVk("NUMLOCK", 0x90), new KeywordVk("SCROLLLOCK", 0x91), 
			new KeywordVk("PRTSC", 0x2c), new KeywordVk("BREAK", 3), 
			new KeywordVk("BACKSPACE", 8), new KeywordVk("BKSP", 8), 
			new KeywordVk("BS", 8), new KeywordVk("CLEAR", 12), 
			new KeywordVk("CAPSLOCK", 20), new KeywordVk("INS", 0x2d), 
			new KeywordVk("INSERT", 0x2d), new KeywordVk("DEL", 0x2e), 
			new KeywordVk("DELETE", 0x2e), new KeywordVk("HELP", 0x2f), 

			new KeywordVk("F1", 0x70), new KeywordVk("F2", 0x71), 
			new KeywordVk("F3", 0x72), new KeywordVk("F4", 0x73), 
			new KeywordVk("F5", 0x74), new KeywordVk("F6", 0x75), 
			new KeywordVk("F7", 0x76), new KeywordVk("F8", 0x77), 
			new KeywordVk("F9", 120), new KeywordVk("F10", 0x79), 
			new KeywordVk("F11", 0x7a), new KeywordVk("F12", 0x7b), 
			new KeywordVk("F13", 0x7c), new KeywordVk("F14", 0x7d), 
			new KeywordVk("F15", 0x7e), new KeywordVk("F16", 0x7f), 
																		 
			new KeywordVk("MULTIPLY", 0x6a), new KeywordVk("ADD", 0x6b), 
			new KeywordVk("SUBTRACT", 0x6d), new KeywordVk("DIVIDE", 0x6f), 
			new KeywordVk("+", 0x6b)
		};

		const int SHIFTVK			= 0x10;
		const int CONTROLVK			= 0x11;
		const int ALTVK				= 0x12;

		const int SHIFTPOS			= 0;
		const int CONTROLPOS		= 1;
		const int ALTPOS			= 2;

		const int KEYEVENTF_KEYDOWN	= 0;
		const int KEYEVENTF_KEYUP	= 2;
		const int KEYEVENTF_SILENT	= 4;

        #region Keyboard P/Invokes
        [DllImport("coredll.dll", SetLastError = true)]
		internal static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        #endregion
    }

    #region Key State Flags
    /// <summary>
    /// KeyStateFlags for Keyboard methods
    /// </summary>
    [Flags()]
    public enum KeyStateFlags : int
    {
        /// <summary>
        /// Key is toggled.
        /// </summary>
        Toggled = 0x0001,
        /// <summary>
        /// 
        /// </summary>
        AsyncDown = 0x0002,		//	 went down since last GetAsync call.
        /// <summary>
        /// Key was previously down.
        /// </summary>
        PrevDown = 0x0040,
        /// <summary>
        /// Key is currently down.
        /// </summary>
        Down = 0x0080,
        /// <summary>
        /// Left or right CTRL key is down.
        /// </summary>
        AnyCtrl = 0x40000000,
        /// <summary>
        /// Left or right SHIFT key is down.
        /// </summary>
        AnyShift = 0x20000000,
        /// <summary>
        /// Left or right ALT key is down.
        /// </summary>
        AnyAlt = 0x10000000,
        /// <summary>
        /// VK_CAPITAL is toggled.
        /// </summary>
        Capital = 0x08000000,
        /// <summary>
        /// Left CTRL key is down.
        /// </summary>
        LeftCtrl = 0x04000000,
        /// <summary>
        /// Left SHIFT key is down.
        /// </summary>
        LeftShift = 0x02000000,
        /// <summary>
        /// Left ALT key is down.
        /// </summary>
        LeftAlt = 0x01000000,
        /// <summary>
        /// Left Windows logo key is down.
        /// </summary>
        LeftWin = 0x00800000,
        /// <summary>
        /// Right CTRL key is down.
        /// </summary>
        RightCtrl = 0x00400000,
        /// <summary>
        /// Right SHIFT key is down
        /// </summary>
        RightShift = 0x00200000,
        /// <summary>
        /// Right ALT key is down
        /// </summary>
        RightAlt = 0x00100000,
        /// <summary>
        /// Right Windows logo key is down.
        /// </summary>
        RightWin = 0x00080000,
        /// <summary>
        /// Corresponding character is dead character.
        /// </summary>
        Dead = 0x00020000,
        /// <summary>
        /// No characters in pCharacterBuffer to translate.
        /// </summary>
        NoCharacter = 0x00010000,
        /// <summary>
        /// Use for language specific shifts.
        /// </summary>
        Language1 = 0x00008000,
        /// <summary>
        /// NumLock toggled state.
        /// </summary>
        NumLock = 0x00001000,
    }
    #endregion
}
