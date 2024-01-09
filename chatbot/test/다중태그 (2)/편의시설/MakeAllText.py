
import time
import os


print("Process Start")


start_time = time.time()

# 텍스트 파일이 모여있는 폴더
directory = "./편의시설/"

# 결과 파일명 지정
outfile_name = "TotalAllText.txt"

# 결과 파일 생성
out_file = open(outfile_name, 'w')

# 폴더 내용물 목록 생성
input_files = os.listdir(directory)

# 폴더 내용을 하나하나 읽어 하나로 합치는 반복문
for filename in input_files:
    # 텍스트 확장자가 아닌파일 걸러내기
    if ".txt" not in filename:
        continue

    # personal_info/파일명.txt 읽기
    file = open(directory + "/" + filename,encoding="utf8")

    # 파일 내용 문자열로 저장
    content = file.read()

    # 문자열로 저장된 내용을 파일에 쓰기
    out_file.write(content + "\n")

    # 읽은 파일 종료 (저장)
    file.close()

# 결과 파일 종료 (저장)
out_file.close()


print("Process Done.")


end_time = time.time()
print("The Job Took " + str(end_time - start_time) + " seconds.")