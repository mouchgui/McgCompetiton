class McgCommon {
    //在js类里面写方法，不要用function关键字
    IsEmpty(obj) {
        if (typeof obj == "undefined" || obj == null || obj == "")
            return false
        else return true
    }

};
export { McgCommon }