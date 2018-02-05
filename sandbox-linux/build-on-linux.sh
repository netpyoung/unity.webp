#!/usr/bin/env bash

# [variable]
ROOT=$(pwd)
DIR_DEST=${ROOT}/output
DIR_LIBWEBP=${ROOT}/libwebp


# [src] libwebp
wget https://storage.googleapis.com/downloads.webmproject.org/releases/webp/libwebp-0.6.1.tar.gz && tar xf libwebp-0.6.1.tar.gz

# compile
cd libwebp-0.6.1 && ./configure && make && make install

mkdir -p ${DIR_DEST}

cp -r src/.libs/* ${DIR_DEST}
