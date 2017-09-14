GIT_ROOT = `git rev-parse --show-toplevel`.strip
VERSION = '0.6.0'
LIBWEBP = "libwebp-#{VERSION}"


desc "default"
task :default do
  sh 'rake -T'
end


desc "install webp"
task :install_webp do
  sh "curl https://storage.googleapis.com/downloads.webmproject.org/releases/webp/#{LIBWEBP}.tar.gz -o #{LIBWEBP}.tar.gz"
  sh "tar xvf #{LIBWEBP}.tar.gz"
  sh "cd #{LIBWEBP} && ./configure && make && make install"
end


desc "sample image"
task :sample_image do
  cp_r 'Lenna.png', "#{GIT_ROOT}/unity_project/Assets/Resources/origin.bytes"

  sh "cwebp -q 80 Lenna.png -o #{GIT_ROOT}/unity_project/Assets/Resources/webp.bytes"
end


desc "update library_osx"
task :update_library_osx do
  Dir.chdir(LIBWEBP) do
    cp_r `realpath src/.libs/libwebp.dylib`.strip, "#{GIT_ROOT}/unity_project/Assets/unity.webp/Plugins/x64/webp.bundle"
  end
end


desc "update library_android"
task :update_library_android do
  ANDROID_MK = 'Android.mk'

  Dir.chdir(LIBWEBP) do
    # to enable build shared library
    File.open(ANDROID_MK, "r") do |orig|
      File.unlink(ANDROID_MK)
      File.open(ANDROID_MK, "w") do |new|
        new.puts 'ENABLE_SHARED := 1'
        new.write(orig.read())
      end
    end

    NDK_BUILD_FPATH = #{Dir.home}/android-ndk-r15c/ndk-build
    sh "#{NDK_BUILD_FPATH} NDK_PROJECT_PATH=#{Dir.pwd} APP_BUILD_SCRIPT=#{Dir.pwd}/#{ANDROID_MK}"
    cp_r "libs/armeabi-v7a/libwebp.so", "#{GIT_ROOT}/unity_project/Assets/unity.webp/Plugins/Android/libs/armeabi-v7a/libwebp.so"
    cp_r "libs/x86/libwebp.so", "#{GIT_ROOT}/unity_project/Assets/unity.webp/Plugins/Android/libs/x86/libwebp.so"
  end
end


desc "update library_ios"
task :update_library_ios do

  Dir.chdir(LIBWEBP) do
    sh "./iosbuild.sh"

    # universal static library
    sh "lipo -create -output libwebp.a iosbuild/iPhoneOS-10.3-aarch64/lib/libwebp.a iosbuild/iPhoneOS-10.3-armv7/lib/libwebp.a"

    cp_r "libwebp.a", "#{GIT_ROOT}/unity_project/Assets/unity.webp/Plugins/iOS/libwebp.a"
  end
end


desc "update library_windows"
task :update_library_windows do
  url = "https://s3.amazonaws.com/resizer-dynamic-downloads/webp/#{VERSION}/x86_64/libwebp.dll"
  output_fpath = "#{GIT_ROOT}/unity_project/Assets/unity.webp/Plugins/x64/webp.dll"
  sh "curl #{url} --output #{output_fpath}"
end


desc "package"
task :package do
  UNITY = '/Applications/Unity/Unity.app/Contents/MacOS/Unity'
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
