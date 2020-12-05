<template>
  <a-modal :title="title" width="40%" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="页面" prop="Module_Id">
          <c-select v-model="entity.Module_Id" url="/MiniPrograms/mini_module/GetOptionList" searchMode="server"></c-select>
        </a-form-model-item>
        <a-form-model-item label="组件名称" prop="Sys_Component_Id">
          <c-select v-model="entity.Sys_Component_Id" url="/MiniPrograms/sys_component/GetOptionList" searchMode="server"></c-select>
        </a-form-model-item>
        <a-form-model-item label="序号" prop="Sort">
          <a-input v-model="entity.Sort" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
  import CSelect from '@/components/CSelect/CSelect'
  export default {
    props: {
      parentObj: Object
    },
    components: {
      CSelect
    },
    data() {
      return {
        layout: {
          labelCol: {
            span: 5
          },
          wrapperCol: {
            span: 18
          }
        },
        visible: false,
        loading: false,
        entity: {},
        rules: {},
        title: ''
      }
    },
    methods: {
      init() {
        this.visible = true
        this.entity = {}
        this.$nextTick(() => {
          this.$refs['form'].clearValidate()
        })
      },
      openForm(id, title) {
        this.init()
        if (id) {
          this.loading = true
          this.$http.post('/MiniPrograms/mini_module_component/GetModuleComponent', {
            id: id
          }).then(resJson => {
            this.loading = false

            this.entity = resJson.Data
          })
        }
      },
      handleSubmit() {
        this.$refs['form'].validate(valid => {
          if (!valid) {
            return
          }
          this.loading = true
          this.$http.post('/MiniPrograms/mini_module_component/SaveData', this.entity).then(resJson => {
            this.loading = false

            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.visible = false

              this.parentObj.getDataList()
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        })
      }
    }
  }
</script>
