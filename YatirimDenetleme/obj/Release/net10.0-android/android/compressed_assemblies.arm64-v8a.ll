; ModuleID = 'compressed_assemblies.arm64-v8a.ll'
source_filename = "compressed_assemblies.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android21"

%struct.CompressedAssemblyDescriptor = type {
	i32, ; uint32_t uncompressed_file_size
	i1, ; bool loaded
	i32 ; uint32_t buffer_offset
}

@compressed_assembly_count = dso_local local_unnamed_addr constant i32 145, align 4

@compressed_assembly_descriptors = dso_local local_unnamed_addr global [145 x %struct.CompressedAssemblyDescriptor] [
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 0; uint32_t buffer_offset
	}, ; 0: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 15624; uint32_t buffer_offset
	}, ; 1: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 31256; uint32_t buffer_offset
	}, ; 2: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 46880; uint32_t buffer_offset
	}, ; 3: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 62504; uint32_t buffer_offset
	}, ; 4: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 78136; uint32_t buffer_offset
	}, ; 5: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 93768; uint32_t buffer_offset
	}, ; 6: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 109400; uint32_t buffer_offset
	}, ; 7: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 125024; uint32_t buffer_offset
	}, ; 8: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 140648; uint32_t buffer_offset
	}, ; 9: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 156280; uint32_t buffer_offset
	}, ; 10: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 171904; uint32_t buffer_offset
	}, ; 11: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 187528; uint32_t buffer_offset
	}, ; 12: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 203152; uint32_t buffer_offset
	}, ; 13: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 218776; uint32_t buffer_offset
	}, ; 14: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 234400; uint32_t buffer_offset
	}, ; 15: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 250024; uint32_t buffer_offset
	}, ; 16: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 265648; uint32_t buffer_offset
	}, ; 17: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 281272; uint32_t buffer_offset
	}, ; 18: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 296904; uint32_t buffer_offset
	}, ; 19: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 312568; uint32_t buffer_offset
	}, ; 20: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 328192; uint32_t buffer_offset
	}, ; 21: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 343824; uint32_t buffer_offset
	}, ; 22: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 359456; uint32_t buffer_offset
	}, ; 23: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 375088; uint32_t buffer_offset
	}, ; 24: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 390760; uint32_t buffer_offset
	}, ; 25: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 406392; uint32_t buffer_offset
	}, ; 26: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 422056; uint32_t buffer_offset
	}, ; 27: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 437680; uint32_t buffer_offset
	}, ; 28: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 453304; uint32_t buffer_offset
	}, ; 29: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 468928; uint32_t buffer_offset
	}, ; 30: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 484552; uint32_t buffer_offset
	}, ; 31: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 500216; uint32_t buffer_offset
	}, ; 32: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 515840; uint32_t buffer_offset
	}, ; 33: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 531472; uint32_t buffer_offset
	}, ; 34: _Microsoft.Android.Resource.Designer
	%struct.CompressedAssemblyDescriptor {
		i32 28672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 537616; uint32_t buffer_offset
	}, ; 35: HarfBuzzSharp
	%struct.CompressedAssemblyDescriptor {
		i32 170496, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 566288; uint32_t buffer_offset
	}, ; 36: HtmlAgilityPack
	%struct.CompressedAssemblyDescriptor {
		i32 777216, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 736784; uint32_t buffer_offset
	}, ; 37: LiveChartsCore
	%struct.CompressedAssemblyDescriptor {
		i32 135168, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 1514000; uint32_t buffer_offset
	}, ; 38: LiveChartsCore.SkiaSharpView
	%struct.CompressedAssemblyDescriptor {
		i32 367616, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 1649168; uint32_t buffer_offset
	}, ; 39: LiveChartsCore.SkiaSharpView.Maui
	%struct.CompressedAssemblyDescriptor {
		i32 14848, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2016784; uint32_t buffer_offset
	}, ; 40: Microsoft.Extensions.Configuration
	%struct.CompressedAssemblyDescriptor {
		i32 6656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2031632; uint32_t buffer_offset
	}, ; 41: Microsoft.Extensions.Configuration.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 46592, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2038288; uint32_t buffer_offset
	}, ; 42: Microsoft.Extensions.DependencyInjection
	%struct.CompressedAssemblyDescriptor {
		i32 26112, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2084880; uint32_t buffer_offset
	}, ; 43: Microsoft.Extensions.DependencyInjection.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 8192, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2110992; uint32_t buffer_offset
	}, ; 44: Microsoft.Extensions.Diagnostics.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 7168, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2119184; uint32_t buffer_offset
	}, ; 45: Microsoft.Extensions.FileProviders.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2126352; uint32_t buffer_offset
	}, ; 46: Microsoft.Extensions.Hosting.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2132496; uint32_t buffer_offset
	}, ; 47: Microsoft.Extensions.Logging
	%struct.CompressedAssemblyDescriptor {
		i32 19456, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2150416; uint32_t buffer_offset
	}, ; 48: Microsoft.Extensions.Logging.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2169872; uint32_t buffer_offset
	}, ; 49: Microsoft.Extensions.Options
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2186768; uint32_t buffer_offset
	}, ; 50: Microsoft.Extensions.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 1928504, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 2195984; uint32_t buffer_offset
	}, ; 51: Microsoft.Maui.Controls
	%struct.CompressedAssemblyDescriptor {
		i32 135432, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 4124488; uint32_t buffer_offset
	}, ; 52: Microsoft.Maui.Controls.Xaml
	%struct.CompressedAssemblyDescriptor {
		i32 861696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 4259920; uint32_t buffer_offset
	}, ; 53: Microsoft.Maui
	%struct.CompressedAssemblyDescriptor {
		i32 54272, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5121616; uint32_t buffer_offset
	}, ; 54: Microsoft.Maui.Essentials
	%struct.CompressedAssemblyDescriptor {
		i32 208696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5175888; uint32_t buffer_offset
	}, ; 55: Microsoft.Maui.Graphics
	%struct.CompressedAssemblyDescriptor {
		i32 97792, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5384584; uint32_t buffer_offset
	}, ; 56: SkiaSharp
	%struct.CompressedAssemblyDescriptor {
		i32 24136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5482376; uint32_t buffer_offset
	}, ; 57: SkiaSharp.HarfBuzz
	%struct.CompressedAssemblyDescriptor {
		i32 50224, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5506512; uint32_t buffer_offset
	}, ; 58: SkiaSharp.Views.Android
	%struct.CompressedAssemblyDescriptor {
		i32 26152, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5556736; uint32_t buffer_offset
	}, ; 59: SkiaSharp.Views.Maui.Controls
	%struct.CompressedAssemblyDescriptor {
		i32 33864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5582888; uint32_t buffer_offset
	}, ; 60: SkiaSharp.Views.Maui.Core
	%struct.CompressedAssemblyDescriptor {
		i32 78848, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5616752; uint32_t buffer_offset
	}, ; 61: SQLite-net
	%struct.CompressedAssemblyDescriptor {
		i32 51712, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5695600; uint32_t buffer_offset
	}, ; 62: SQLitePCLRaw.core
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5747312; uint32_t buffer_offset
	}, ; 63: SQLitePCLRaw.provider.e_sqlite3
	%struct.CompressedAssemblyDescriptor {
		i32 73728, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5784176; uint32_t buffer_offset
	}, ; 64: Xamarin.AndroidX.Activity
	%struct.CompressedAssemblyDescriptor {
		i32 583680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 5857904; uint32_t buffer_offset
	}, ; 65: Xamarin.AndroidX.AppCompat
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 6441584; uint32_t buffer_offset
	}, ; 66: Xamarin.AndroidX.AppCompat.AppCompatResources
	%struct.CompressedAssemblyDescriptor {
		i32 18944, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 6459504; uint32_t buffer_offset
	}, ; 67: Xamarin.AndroidX.CardView
	%struct.CompressedAssemblyDescriptor {
		i32 22528, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 6478448; uint32_t buffer_offset
	}, ; 68: Xamarin.AndroidX.Collection.Jvm
	%struct.CompressedAssemblyDescriptor {
		i32 78336, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 6500976; uint32_t buffer_offset
	}, ; 69: Xamarin.AndroidX.CoordinatorLayout
	%struct.CompressedAssemblyDescriptor {
		i32 596992, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 6579312; uint32_t buffer_offset
	}, ; 70: Xamarin.AndroidX.Core
	%struct.CompressedAssemblyDescriptor {
		i32 26624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7176304; uint32_t buffer_offset
	}, ; 71: Xamarin.AndroidX.CursorAdapter
	%struct.CompressedAssemblyDescriptor {
		i32 9728, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7202928; uint32_t buffer_offset
	}, ; 72: Xamarin.AndroidX.CustomView
	%struct.CompressedAssemblyDescriptor {
		i32 47104, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7212656; uint32_t buffer_offset
	}, ; 73: Xamarin.AndroidX.DrawerLayout
	%struct.CompressedAssemblyDescriptor {
		i32 236032, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7259760; uint32_t buffer_offset
	}, ; 74: Xamarin.AndroidX.Fragment
	%struct.CompressedAssemblyDescriptor {
		i32 23552, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7495792; uint32_t buffer_offset
	}, ; 75: Xamarin.AndroidX.Lifecycle.Common.Jvm
	%struct.CompressedAssemblyDescriptor {
		i32 18944, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7519344; uint32_t buffer_offset
	}, ; 76: Xamarin.AndroidX.Lifecycle.LiveData.Core
	%struct.CompressedAssemblyDescriptor {
		i32 32768, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7538288; uint32_t buffer_offset
	}, ; 77: Xamarin.AndroidX.Lifecycle.ViewModel.Android
	%struct.CompressedAssemblyDescriptor {
		i32 13824, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7571056; uint32_t buffer_offset
	}, ; 78: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.Android
	%struct.CompressedAssemblyDescriptor {
		i32 39424, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7584880; uint32_t buffer_offset
	}, ; 79: Xamarin.AndroidX.Loader
	%struct.CompressedAssemblyDescriptor {
		i32 92672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7624304; uint32_t buffer_offset
	}, ; 80: Xamarin.AndroidX.Navigation.Common.Android
	%struct.CompressedAssemblyDescriptor {
		i32 19456, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7716976; uint32_t buffer_offset
	}, ; 81: Xamarin.AndroidX.Navigation.Fragment
	%struct.CompressedAssemblyDescriptor {
		i32 65536, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7736432; uint32_t buffer_offset
	}, ; 82: Xamarin.AndroidX.Navigation.Runtime.Android
	%struct.CompressedAssemblyDescriptor {
		i32 27136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7801968; uint32_t buffer_offset
	}, ; 83: Xamarin.AndroidX.Navigation.UI
	%struct.CompressedAssemblyDescriptor {
		i32 457728, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 7829104; uint32_t buffer_offset
	}, ; 84: Xamarin.AndroidX.RecyclerView
	%struct.CompressedAssemblyDescriptor {
		i32 12288, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 8286832; uint32_t buffer_offset
	}, ; 85: Xamarin.AndroidX.SavedState.SavedState.Android
	%struct.CompressedAssemblyDescriptor {
		i32 41984, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 8299120; uint32_t buffer_offset
	}, ; 86: Xamarin.AndroidX.SwipeRefreshLayout
	%struct.CompressedAssemblyDescriptor {
		i32 62976, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 8341104; uint32_t buffer_offset
	}, ; 87: Xamarin.AndroidX.ViewPager
	%struct.CompressedAssemblyDescriptor {
		i32 40448, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 8404080; uint32_t buffer_offset
	}, ; 88: Xamarin.AndroidX.ViewPager2
	%struct.CompressedAssemblyDescriptor {
		i32 675840, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 8444528; uint32_t buffer_offset
	}, ; 89: Xamarin.Google.Android.Material
	%struct.CompressedAssemblyDescriptor {
		i32 90624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9120368; uint32_t buffer_offset
	}, ; 90: Xamarin.Kotlin.StdLib
	%struct.CompressedAssemblyDescriptor {
		i32 28672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9210992; uint32_t buffer_offset
	}, ; 91: Xamarin.KotlinX.Coroutines.Core.Jvm
	%struct.CompressedAssemblyDescriptor {
		i32 91648, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9239664; uint32_t buffer_offset
	}, ; 92: Xamarin.KotlinX.Serialization.Core.Jvm
	%struct.CompressedAssemblyDescriptor {
		i32 169472, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9331312; uint32_t buffer_offset
	}, ; 93: YatirimDenetleme
	%struct.CompressedAssemblyDescriptor {
		i32 27648, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9500784; uint32_t buffer_offset
	}, ; 94: System.Collections.Concurrent
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9528432; uint32_t buffer_offset
	}, ; 95: System.Collections.NonGeneric
	%struct.CompressedAssemblyDescriptor {
		i32 20480, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9546352; uint32_t buffer_offset
	}, ; 96: System.Collections.Specialized
	%struct.CompressedAssemblyDescriptor {
		i32 35328, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9566832; uint32_t buffer_offset
	}, ; 97: System.Collections
	%struct.CompressedAssemblyDescriptor {
		i32 12288, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9602160; uint32_t buffer_offset
	}, ; 98: System.ComponentModel.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 94720, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9614448; uint32_t buffer_offset
	}, ; 99: System.ComponentModel.TypeConverter
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9709168; uint32_t buffer_offset
	}, ; 100: System.ComponentModel
	%struct.CompressedAssemblyDescriptor {
		i32 12288, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9714288; uint32_t buffer_offset
	}, ; 101: System.Console
	%struct.CompressedAssemblyDescriptor {
		i32 51200, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9726576; uint32_t buffer_offset
	}, ; 102: System.Diagnostics.DiagnosticSource
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9777776; uint32_t buffer_offset
	}, ; 103: System.Diagnostics.TraceSource
	%struct.CompressedAssemblyDescriptor {
		i32 11776, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9794160; uint32_t buffer_offset
	}, ; 104: System.Drawing.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9805936; uint32_t buffer_offset
	}, ; 105: System.Drawing
	%struct.CompressedAssemblyDescriptor {
		i32 61952, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9811056; uint32_t buffer_offset
	}, ; 106: System.Formats.Asn1
	%struct.CompressedAssemblyDescriptor {
		i32 22016, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9873008; uint32_t buffer_offset
	}, ; 107: System.IO.Compression.Brotli
	%struct.CompressedAssemblyDescriptor {
		i32 32768, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9895024; uint32_t buffer_offset
	}, ; 108: System.IO.Compression
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9927792; uint32_t buffer_offset
	}, ; 109: System.IO.Pipelines
	%struct.CompressedAssemblyDescriptor {
		i32 356864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 9933936; uint32_t buffer_offset
	}, ; 110: System.Linq.Expressions
	%struct.CompressedAssemblyDescriptor {
		i32 65024, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10290800; uint32_t buffer_offset
	}, ; 111: System.Linq
	%struct.CompressedAssemblyDescriptor {
		i32 16384, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10355824; uint32_t buffer_offset
	}, ; 112: System.Memory
	%struct.CompressedAssemblyDescriptor {
		i32 381440, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10372208; uint32_t buffer_offset
	}, ; 113: System.Net.Http
	%struct.CompressedAssemblyDescriptor {
		i32 28160, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10753648; uint32_t buffer_offset
	}, ; 114: System.Net.NameResolution
	%struct.CompressedAssemblyDescriptor {
		i32 26624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10781808; uint32_t buffer_offset
	}, ; 115: System.Net.NetworkInformation
	%struct.CompressedAssemblyDescriptor {
		i32 72704, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10808432; uint32_t buffer_offset
	}, ; 116: System.Net.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 89600, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10881136; uint32_t buffer_offset
	}, ; 117: System.Net.Requests
	%struct.CompressedAssemblyDescriptor {
		i32 147456, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 10970736; uint32_t buffer_offset
	}, ; 118: System.Net.Security
	%struct.CompressedAssemblyDescriptor {
		i32 109568, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11118192; uint32_t buffer_offset
	}, ; 119: System.Net.Sockets
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11227760; uint32_t buffer_offset
	}, ; 120: System.Net.WebHeaderCollection
	%struct.CompressedAssemblyDescriptor {
		i32 10752, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11236976; uint32_t buffer_offset
	}, ; 121: System.Net.WebProxy
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11247728; uint32_t buffer_offset
	}, ; 122: System.Numerics.Vectors
	%struct.CompressedAssemblyDescriptor {
		i32 20480, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11252848; uint32_t buffer_offset
	}, ; 123: System.ObjectModel
	%struct.CompressedAssemblyDescriptor {
		i32 75264, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11273328; uint32_t buffer_offset
	}, ; 124: System.Private.Uri
	%struct.CompressedAssemblyDescriptor {
		i32 2027520, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 11348592; uint32_t buffer_offset
	}, ; 125: System.Private.Xml
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13376112; uint32_t buffer_offset
	}, ; 126: System.Runtime.InteropServices
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13385328; uint32_t buffer_offset
	}, ; 127: System.Runtime.Loader
	%struct.CompressedAssemblyDescriptor {
		i32 79360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13390448; uint32_t buffer_offset
	}, ; 128: System.Runtime.Numerics
	%struct.CompressedAssemblyDescriptor {
		i32 14848, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13469808; uint32_t buffer_offset
	}, ; 129: System.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 165888, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13484656; uint32_t buffer_offset
	}, ; 130: System.Security.Cryptography
	%struct.CompressedAssemblyDescriptor {
		i32 29696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13650544; uint32_t buffer_offset
	}, ; 131: System.Text.Encodings.Web
	%struct.CompressedAssemblyDescriptor {
		i32 379392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 13680240; uint32_t buffer_offset
	}, ; 132: System.Text.Json
	%struct.CompressedAssemblyDescriptor {
		i32 331776, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14059632; uint32_t buffer_offset
	}, ; 133: System.Text.RegularExpressions
	%struct.CompressedAssemblyDescriptor {
		i32 24064, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14391408; uint32_t buffer_offset
	}, ; 134: System.Threading.Channels
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14415472; uint32_t buffer_offset
	}, ; 135: System.Threading.Thread
	%struct.CompressedAssemblyDescriptor {
		i32 12288, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14420592; uint32_t buffer_offset
	}, ; 136: System.Threading
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14432880; uint32_t buffer_offset
	}, ; 137: System.Xml.ReaderWriter
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14438512; uint32_t buffer_offset
	}, ; 138: System.Xml.XPath
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14443632; uint32_t buffer_offset
	}, ; 139: System.Xml.XmlSerializer
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14448752; uint32_t buffer_offset
	}, ; 140: System
	%struct.CompressedAssemblyDescriptor {
		i32 2137600, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 14453872; uint32_t buffer_offset
	}, ; 141: System.Private.CoreLib
	%struct.CompressedAssemblyDescriptor {
		i32 171008, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 16591472; uint32_t buffer_offset
	}, ; 142: Java.Interop
	%struct.CompressedAssemblyDescriptor {
		i32 22368, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 16762480; uint32_t buffer_offset
	}, ; 143: Mono.Android.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 2227712, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		i32 16784848; uint32_t buffer_offset
	} ; 144: Mono.Android
], align 4

@uncompressed_assemblies_data_size = dso_local local_unnamed_addr constant i32 19012560, align 4

@uncompressed_assemblies_data_buffer = dso_local local_unnamed_addr global [19012560 x i8] zeroinitializer, align 1

; Metadata
!llvm.module.flags = !{!0, !1, !7, !8, !9, !10}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/10.0.1xx @ d549e1dc4e2a083b08b4f24cb5495e81b99d79b5"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"branch-target-enforcement", i32 0}
!8 = !{i32 1, !"sign-return-address", i32 0}
!9 = !{i32 1, !"sign-return-address-all", i32 0}
!10 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
