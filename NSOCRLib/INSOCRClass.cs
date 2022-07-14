using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NSOCRLib
{
	[ComImport]
	[CompilerGenerated]
	[Guid("6FDE2340-E079-44BF-B3E3-7E99F243E27A")]
	[TypeIdentifier]
	public interface INSOCRClass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1)]
		int Engine_Initialize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		int Engine_Uninitialize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		int Cfg_Create(out int CfgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		int Cfg_Destroy([In] int CfgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		int Cfg_LoadOptions([In] int CfgObj, [In] [MarshalAs(UnmanagedType.BStr)] string FileName);

		void _VtblGap1_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		int Cfg_GetOption([In] int CfgObj, [In] int BlockType, [In] [MarshalAs(UnmanagedType.BStr)] string OptionPath, [MarshalAs(UnmanagedType.BStr)] out string OptionValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		int Cfg_SetOption([In] int CfgObj, [In] int BlockType, [In] [MarshalAs(UnmanagedType.BStr)] string OptionPath, [In] [MarshalAs(UnmanagedType.BStr)] string OptionValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		int Ocr_Create([In] int CfgObj, out int OcrObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(10)]
		int Ocr_Destroy([In] int OcrObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(12)]
		int Img_Create([In] int OcrObj, out int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(13)]
		int Img_Destroy([In] int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(14)]
		int Img_LoadFile([In] int ImgObj, [In] [MarshalAs(UnmanagedType.BStr)] string FileName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(15)]
		int Img_DeleteAllBlocks([In] int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(16)]
		int Img_AddBlock([In] int ImgObj, [In] int Xpos, [In] int Ypos, [In] int Width, [In] int Height, out int BlkObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(17)]
		int Img_DeleteBlock([In] int ImgObj, [In] int BlkObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(18)]
		int Img_GetBlockCnt([In] int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(19)]
		int Img_GetBlock([In] int ImgObj, [In] int BlockIndex, out int BlkObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(20)]
		int Img_GetImgText([In] int ImgObj, [MarshalAs(UnmanagedType.BStr)] out string TextStr, [In] int Flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(21)]
		int Blk_GetRect([In] int BlkObj, out int Xpos, out int Ypos, out int Width, out int Height);

		void _VtblGap2_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(23)]
		int Blk_GetLineCnt([In] int BlkObj);

		void _VtblGap3_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(25)]
		int Blk_GetWordCnt([In] int BlkObj, [In] int LineIndex);

		void _VtblGap4_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(27)]
		int Blk_GetCharCnt([In] int BlkObj, [In] int LineIndex, [In] int WordIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(28)]
		int Blk_GetCharRect([In] int BlkObj, [In] int LineIndex, [In] int WordIndex, [In] int CharIndex, out int Xpos, out int Ypos, out int Width, out int Height);

		void _VtblGap5_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(30)]
		int Img_GetSize([In] int ImgObj, out int Width, out int Height);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(31)]
		int Img_DrawToDC([In] int ImgObj, [In] int HandleDC, [In] int X, [In] int Y, [In] [Out] ref int Width, [In] [Out] ref int Height, [In] int Flags);

		void _VtblGap6_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(33)]
		int Img_GetPageCount([In] int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(34)]
		int Img_SetPage([In] int ImgObj, [In] int PageIndex);

		void _VtblGap7_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(36)]
		int Blk_GetType([In] int BlkObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(37)]
		int Blk_SetType([In] int BlkObj, [In] int BlockType);

		void _VtblGap8_3();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(41)]
		int Img_LoadBmpData([In] int ImgObj, [In] [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] ref Array Bytes, [In] int Width, [In] int Height, [In] int Flags, [In] int Stride);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(42)]
		int Img_LoadFromMemory([In] int ImgObj, [In] [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] ref Array Bytes, [In] int DataSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(43)]
		int Engine_GetVersion([MarshalAs(UnmanagedType.BStr)] out string VerStr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(44)]
		int Img_OCR([In] int ImgObj, [In] int FirstStep, [In] int LastStep, [In] int Flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(45)]
		int Blk_Inversion([In] int BlkObj, [In] int Inversion);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(46)]
		int Blk_Rotation([In] int BlkObj, [In] int Rotation);

		void _VtblGap9_3();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(50)]
		int Img_SaveBlocks([In] int ImgObj, [In] [MarshalAs(UnmanagedType.BStr)] string FileName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(51)]
		int Img_LoadBlocks([In] int ImgObj, [In] [MarshalAs(UnmanagedType.BStr)] string FileName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(52)]
		int Img_GetSkewAngle([In] int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(53)]
		int Img_GetPixLineCnt([In] int ImgObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(54)]
		int Img_GetPixLine([In] int ImgObj, [In] int LineInd, out int X1pos, out int Y1pos, out int X2pos, out int Y2pos, out int Width);

		void _VtblGap10_1();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(56)]
		int Svr_Destroy([In] int SvrObj);

		void _VtblGap11_2();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(59)]
		int Svr_SaveToFile([In] int SvrObj, [In] [MarshalAs(UnmanagedType.BStr)] string FileName);

		void _VtblGap12_3();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(63)]
		int Scan_Create([In] int CfgObj, out int ScanObj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(64)]
		int Scan_Enumerate([In] int ScanObj, [MarshalAs(UnmanagedType.BStr)] out string ScannerNames, [In] int Flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(65)]
		int Scan_ScanToImg([In] int ScanObj, [In] int ImgObj, [In] int ScannerIndex, [In] int Flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(66)]
		int Scan_Destroy([In] int ScanObj);

		void _VtblGap13_6();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(73)]
		int Ocr_ProcessPages([In] int ImgObj, [In] int SvrObj, [In] int PageIndexStart, [In] int PageIndexEnd, [In] int OcrObjCnt, int Flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(74)]
		int Img_GetProperty([In] int ImgObj, [In] int PropertyID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(75)]
		int Engine_InitializeAdvanced(out int CfgObj, out int OcrObj, out int ImgObj);

		void _VtblGap14_2();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(78)]
		int Blk_SetWordRegEx([In] int BlkObj, [In] int LineIndex, [In] int WordIndex, [In] [MarshalAs(UnmanagedType.BStr)] string RegEx, [In] int Flags);

		void _VtblGap15_12();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(91)]
		int Engine_SetLicenseKey([MarshalAs(UnmanagedType.BStr)] string LicenseKey);
	}
}
