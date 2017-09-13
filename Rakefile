GIT_ROOT = `git rev-parse --show-toplevel`.strip

desc "default"
task :default do
  sh 'rake -T'
end

libwebp = 'libwebp-0.6.0'

desc "install webp"
task :install_webp do
  sh "curl https://storage.googleapis.com/downloads.webmproject.org/releases/webp/#{libwebp}.tar.gz -o #{libwebp}.tar.gz"
  sh "tar xvf #{libwebp}.tar.gz"
  sh "cd #{libwebp} && ./configure && make && make install"
end

desc "update library"
task :update_library do
  Dir.chdir(libwebp) do
    cp_r `realpath src/.libs/libwebp.dylib`.strip, "#{GIT_ROOT}/unity_project/Assets/unity.webp/Plugins/x64/webp.bundle"
  end
end


desc "sample image"
task :sample_image do
  cp_r 'Lenna.png', "#{GIT_ROOT}/unity_project/Assets/Resources/origin.bytes"

  sh "cwebp -q 80 Lenna.png -o #{GIT_ROOT}/unity_project/Assets/Resources/webp.bytes"
end
