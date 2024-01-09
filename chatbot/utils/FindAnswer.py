class FindAnswer:
    def __init__(self, db):
        self.db = db

    # 검색 쿼리 생성
    def _make_query(self, intent_name, ner_tags):
        sql = "select * from chatbot_train_data"
        if intent_name is not None and ner_tags is None:
            sql = sql + " where intent='{}' ".format(intent_name)

        elif intent_name is not None and ner_tags is not None:
            where = " where intent='%s' " % intent_name
            if len(ner_tags) > 0:
                where += "and ("
                for ne in ner_tags:
                    where += " ner like '%{}%' or ".format(ne)
                where = where[:-3] + ')'
            sql = sql + where

        # 동일한 답변이 2개 이상인 경우 랜덤으로 선택
        sql = sql + " order by rand() limit 1"
        return sql

    # 답변 검색
    def search(self, intent_name, ner_tags):
        # 의도명과 개체명으로 답변 검색
        sql = self._make_query(intent_name, ner_tags)
        answer = self.db.select_one(sql)

        # 검색되는 답변이 없으면 의도명만 검색
        if answer is None:
            sql = self._make_query(intent_name, None)
            answer = self.db.select_one(sql)

        return answer['answer'], answer['answer_image']

    # NER 태그를 실제 입력된 단어로 변환
    def tag_to_word(self, ner_predicts, answer):
        item =  {'O': 1, 'B_BOOK': 2, 'B_CAFE': 3, 'B_SOl': 4, 'B_MAJ': 5, 'B_ROT': 6, 'B_BIT': 7, 'B_AT': 8, 'B_GUI': 9,
             'B_CER': 10, 'NNG': 11, 'B_CLUB': 12, 'B_AC': 13, 'B_AL': 14, 'B_JOB': 15, 'B_AB': 16, 'B_AD': 17,
             'B_AE': 18, 'B_AF': 19, 'B_AG': 20, 'B_AH': 21, 'B_AI': 22, 'B_AJ': 23, 'B_AK': 24, 'B_AN': 25, 'B_AP': 26,
             'B_AQ': 27, 'B_AR': 28, 'B_AS': 29, 'NNP': 30}

        for word, tag in ner_predicts:
            # 변환해야 하는 태그가 있는 경우 추가

            if tag in item.keys():
                answer = answer.replace(tag, word)

        answer = answer.replace('{', '')
        answer = answer.replace('}', '')
        return answer
