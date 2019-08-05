#!/bin/bash

# [variable]
ROOT=$(pwd)
DIR_DEST=${ROOT}/output
DIR_LIBWEBP=${ROOT}/libwebp


# [src] libwebp
wget https://storage.googleapis.com/downloads.webmproject.org/releases/webp/libwebp-1.0.0.tar.gz && tar xf libwebp-1.0.0.tar.gz

# compile
cd libwebp-1.0.0
./configure --prefix=`pwd`/.lib --enable-everything --disable-static
make && make install

mkdir -p ${DIR_DEST}

cp -r .lib/* ${DIR_DEST}
