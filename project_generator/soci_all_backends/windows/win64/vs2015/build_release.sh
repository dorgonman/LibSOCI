set -e

CONFIG_PATH=$(pwd)/release.xml
echo CONFIG_PATH:${CONFIG_PATH}
pushd ../../../../../../../python/HorizonBuildTool/HorizonBuildTool/

python Source/HorizonCMakeBuild/Main.py --clean --config=${CONFIG_PATH}
python Source/HorizonCMakeBuild/Main.py --config=${CONFIG_PATH}
popd


cp -r ../../../../../intermediate/project/win64/vs2015/Release/lib/Release/ \
	  ../../../../../libs/win64/vs2015

cp -rf ../../../../../intermediate/project/win64/vs2015/Release/include/ \
	../../../../../