import {
  Axios
} from "@/utils/plugin/axios-plugin"

let permissions = []
let inited = false

let OperatorCache = {
  info: {},
  projectList: [],
  inited() {
    return inited
  },
  init(callBack) {
    if (inited)
      callBack()
    else {
      Axios.post('/Base_Manage/Home/GetOperatorInfo').then(resJson => {
        this.info = resJson.Data.UserInfo
        this.projectList = resJson.Data.ProjectList
        permissions = resJson.Data.Permissions
        inited = true
        callBack()
      })
    }
  },
  hasPermission(thePermission) {
    return permissions.includes(thePermission)
  },
  clear() {
    inited = false
    permissions = []
    this.info = {}
  },
  changeProject(project_id) {
    this.loading = true
    Axios.post('/Base_Manage/Base_User/UpdateUserLastInterviewProject', {
      "id": project_id
    }).then(resJson => {
      this.loading = false
      location.reload()
    })
  }
}

export default OperatorCache
