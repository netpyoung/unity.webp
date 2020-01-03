GIT_ROOT = `git rev-parse --show-toplevel`.strip
VERSION = '1.0.0'
LIBWEBP = "libwebp"


desc "default"
task :default do
  sh 'rake -T'
end

desc "sample image"
task :sample_image do
  cp_r 'Lenna.png', "#{GIT_ROOT}/unity_project/Assets/Resources/origin.bytes"
  sh "cwebp -q 80 Lenna.png -o #{GIT_ROOT}/unity_project/Assets/Resources/webp.bytes"
end

desc "update library_macos"
task :update_library_macos do
  build_dir = 'build/macos'
  lib_dir = "#{GIT_ROOT}/lib/macos_64"

  FileUtils.mkdir_p(build_dir) unless File.directory?(build_dir)
  FileUtils.mkdir_p(lib_dir) unless File.directory?(lib_dir)

  Dir.chdir(build_dir) do
    sh 'git clone https://github.com/webmproject/libwebp.git'
    Dir.chdir('libwebp') do
      sh "git checkout #{VERSION}"
      sh './autogen.sh'
      sh "./configure --prefix=`pwd`/.lib --enable-everything --disable-static"
      sh 'make && make install'
    end

    cp_r `realpath libwebp/.lib/lib/libwebp.dylib`.strip, lib_dir
    cp_r `realpath libwebp/.lib/lib/libwebpdecoder.dylib`.strip, lib_dir
    cp_r `realpath libwebp/.lib/lib/libwebpdemux.dylib`.strip, lib_dir
    cp_r `realpath libwebp/.lib/lib/libwebpmux.dylib`.strip, lib_dir
  end
end


desc "update library_android"
task :update_library_android do
  # android-ndk: https://developer.android.com/ndk/downloads
  ANDROID_MK = 'Android.mk'

  build_dir = 'build/android'
  lib_armeabi_v7a_dir = "#{GIT_ROOT}/lib/android/armeabi-v7a"
  lib_x86_dir = "#{GIT_ROOT}/lib/android/x86"
  lib_x86_64_dir = "#{GIT_ROOT}/lib/android/x86_64"
  lib_arm64_v8a_dir  = "#{GIT_ROOT}/lib/android/arm64-v8a"

  FileUtils.mkdir_p(build_dir) unless File.directory?(build_dir)
  FileUtils.mkdir_p(lib_x86_dir) unless File.directory?(lib_x86_dir)
  FileUtils.mkdir_p(lib_armeabi_v7a_dir) unless File.directory?(lib_armeabi_v7a_dir)
  FileUtils.mkdir_p(lib_arm64_v8a_dir) unless File.directory?(lib_arm64_v8a_dir)
  FileUtils.mkdir_p(lib_x86_64_dir) unless File.directory?(lib_x86_64_dir)

  Dir.chdir(build_dir) do
    sh 'git clone https://github.com/webmproject/libwebp.git'

    Dir.chdir('libwebp') do
      sh "git checkout #{VERSION}"

      # to enable build shared library
      File.open(ANDROID_MK, "r") do |orig|
        # File.unlink(ANDROID_MK)
        o = orig.read()
        File.open(ANDROID_MK, "w") do |new|
          new.puts 'ENABLE_SHARED := 1'
          new.write(o)
        end
      end

      NDK_BUILD_FPATH = "#{Dir.home}/android-ndk-r20/ndk-build"
      sh "#{NDK_BUILD_FPATH} NDK_PROJECT_PATH=#{Dir.pwd} APP_BUILD_SCRIPT=#{Dir.pwd}/#{ANDROID_MK}"

      ['libwebp.so', 'libwebpdecoder.so', 'libwebpdemux.so', 'libwebpmux.so'].each do |so|
        cp_r "libs/armeabi-v7a/#{so}", lib_armeabi_v7a_dir
        cp_r "libs/arm64-v8a/#{so}", lib_arm64_v8a_dir
        cp_r "libs/x86/#{so}", lib_x86_dir
        cp_r "libs/x86_64/#{so}", lib_x86_64_dir
      end
    end
  end
end


desc "update library_ios"
task :update_library_ios do
  build_dir = 'build/ios'
  lib_dir  = "#{GIT_ROOT}/lib/ios-universal"
  lib_simulator_dir  = "#{GIT_ROOT}/lib/ios-simulator"
  version = '12.4'

  FileUtils.mkdir_p(build_dir) unless File.directory?(build_dir)
  FileUtils.mkdir_p(lib_dir) unless File.directory?(lib_dir)
  FileUtils.mkdir_p(lib_simulator_dir) unless File.directory?(lib_simulator_dir)
  Dir.chdir(build_dir) do
    sh 'git clone https://github.com/webmproject/libwebp.git'

    Dir.chdir('libwebp') do
      sh "git checkout #{VERSION}"
      sh "./iosbuild.sh"

      # universal static library
      ['libwebp.a', 'libwebpdecoder.a', 'libwebpdemux.a'].each do |a|
        sh "lipo -create -output #{a} iosbuild/iPhoneOS-#{version}-aarch64/lib/#{a} iosbuild/iPhoneOS-#{version}-armv7/lib/#{a}"
        cp_r a, lib_dir
      end

      # simulator static library
      ['libwebp.a', 'libwebpdecoder.a', 'libwebpdemux.a'].each do |a|
        # sh "lipo -create -output #{a} iosbuild/iPhoneSimulator-#{version}-x86_64/lib/#{a} iosbuild/iPhoneSimulator-#{version}-i386/lib/#{a}"
        cp_r "iosbuild/iPhoneSimulator-#{version}-x86_64/lib/#{a}", lib_simulator_dir
      end
    end
  end
end


desc "update library_windows"
task :update_library_windows do
  # Windows + R
  # %comspec% /k "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\vcvars64.bat"

  build_dir = 'build/win_64'
  lib_dir = "#{GIT_ROOT}/lib/win_64"

  FileUtils.mkdir_p(build_dir) unless File.directory?(build_dir)
  FileUtils.mkdir_p(lib_dir) unless File.directory?(lib_dir)
  Dir.chdir(build_dir) do
    sh 'git clone https://github.com/webmproject/libwebp.git'
    Dir.chdir('libwebp') do
      sh "git checkout #{VERSION}"
      sh 'nmake /f Makefile.vc CFG=release-dynamic RTLIBCFG=dynamic OBJDIR=output'
    end
    cp 'libwebp/output/release-dynamic/x64/bin/libwebp.dll', lib_dir
    cp 'libwebp/output/release-dynamic/x64/bin/libwebpdecoder.dll', lib_dir
    cp 'libwebp/output/release-dynamic/x64/bin/libwebpdemux.dll', lib_dir
  end
end


desc "package"
task :package do
  UNITY = '/Applications/Unity/Hub/Editor/2019.1.10f1/Unity.app/Contents/MacOS/Unity'
  UNITY_PRJ_DIR = "#{GIT_ROOT}/unity_project"
  UNITY_PACKAGE_METHOD = 'PackageTool.UpdatePackage'

  build_cmd = [
    UNITY,
    "-quit -batchmode",
    "-nographics",
    "-buildTarget android",
    "-projectPath #{UNITY_PRJ_DIR}",
    "-executeMethod #{UNITY_PACKAGE_METHOD}"
  ].join(' ');

  sh build_cmd
end
