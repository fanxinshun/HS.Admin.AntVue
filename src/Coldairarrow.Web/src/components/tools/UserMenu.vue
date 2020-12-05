<template>
  <div class="user-wrapper">
    <div class="content-box">
      <span>项目地:</span>
      <!-- <a href="https://pro.loacg.com/docs/getting-started" target="_blank">
        <span class="action">
          <a-icon type="question-circle-o"></a-icon>
        </span>
      </a> -->
      <!-- <notice-icon class="action" /> -->
      <a-dropdown>
        <span class="action ant-dropdown-link">
          <a-avatar size="small" icon="appstore" />
          <span>{{ UserInfo.projectName }}</span>
        </span>
        <a-menu slot="overlay">
          <a-menu-item v-for="item in ProjectList" :key="item.Id" @click="projectSelected(item.Id)">{{ item.Project_Name }}
          </a-menu-item>
        </a-menu>
      </a-dropdown>
      <a-dropdown>
        <span class="action ant-dropdown-link user-dropdown-menu">
          <a-avatar size="small" icon="user" />
          <span>{{ UserInfo.UserName }}</span>
        </span>
        <a-menu slot="overlay" class="user-dropdown-menu-wrapper">
          <a-menu-item key="1">
            <a href="javascript:;" @click="handleChangePwd()">
              <a-icon type="lock" />
              <span>修改密码</span>
            </a>
            <change-pwd-form ref="changePwd"></change-pwd-form>
          </a-menu-item>
          <a-menu-divider />
          <a-menu-item key="3">
            <a href="javascript:;" @click="handleLogout()">
              <a-icon type="logout" />
              <span>退出登录</span>
            </a>
          </a-menu-item>
        </a-menu>
      </a-dropdown>
    </div>
  </div>
</template>

<script>
  // import NoticeIcon from '@/components/NoticeIcon'
  // import { mapActions, mapGetters } from 'vuex'
  import OperatorCache from '@/utils/cache/OperatorCache'
  import TokenCache from '@/utils/cache/TokenCache'
  import ChangePwdForm from './ChangePwdForm'

  export default {
    name: 'UserMenu',
    components: {
      // NoticeIcon
      ChangePwdForm
    },
    mounted() {
      this.op()
    },
    data() {
      return {
        ProjectList: [],
        UserInfo: {
          projectName: '',
          UserName: ''
        }
      }
    },
    methods: {
      op() {
        this.UserInfo.UserName = OperatorCache.info.UserName
        this.ProjectList = OperatorCache.projectList

        let lastProject = this.ProjectList.find(x => x.Id == OperatorCache.info.Last_Interview_Project)
        if (lastProject) {
          this.UserInfo.projectName = lastProject.Project_Name
        }
      },
      // ...mapActions(['Logout']),
      // ...mapGetters(['nickname', 'avatar']),
      handleLogout() {
        const that = this
        this.$confirm({
          title: '提示',
          content: '真的要注销登录吗 ?',
          onOk() {
            TokenCache.deleteToken()
            OperatorCache.clear()
            location.reload()
            // that.$router.push({ path: '/user/login' })
          }
        })
      },
      handleChangePwd() {
        this.$refs.changePwd.open()
      },
      projectSelected(project_id) {
        OperatorCache.changeProject(project_id)
      }
    }
  }
</script>
