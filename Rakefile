## ref: https://github.com/netpyoung/prebuilt-libwebp
VERSION = 'v0.0.1'


desc "default"
task :default do
  sh 'rake -T'
end

desc "sample image"
task :sample_image do
  cp_r 'Lenna.png', "#{GIT_ROOT}/unity_project/Assets/Resources/origin.bytes"
  sh "cwebp -q 80 Lenna.png -o #{GIT_ROOT}/unity_project/Assets/Resources/webp.bytes"
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

## =================================================================
desc "update library_windows"
task :update_library_windows do
  sh "curl -LO https://github.com/netpyoung/prebuilt-libwebp/releases/download/#{VERSION}/windows.zip"
  # sh "7z x windows.zip -o\_lib/"
end

desc "update library_linux"
task :update_library_linux do
  sh "curl -LO https://github.com/netpyoung/prebuilt-libwebp/releases/download/#{VERSION}/linux.zip"
end

desc "update library_android"
task :update_library_android do
  sh "curl -LO https://github.com/netpyoung/prebuilt-libwebp/releases/download/#{VERSION}/android.zip"
end

desc "update library_macos"
task :update_library_macos do
  sh "curl -LO https://github.com/netpyoung/prebuilt-libwebp/releases/download/#{VERSION}/macos.zip"
end

desc "update library_ios"
task :update_library_ios do
  sh "curl -LO https://github.com/netpyoung/prebuilt-libwebp/releases/download/#{VERSION}/ios.zip"
end
