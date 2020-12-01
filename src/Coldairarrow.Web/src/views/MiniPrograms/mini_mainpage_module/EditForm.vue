<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="项目地" prop="OwnerShip">
          <a-input v-model="entity.OwnerShip" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="模组编码" prop="Module_Code">
          <a-input v-model="entity.Module_Code" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="模组名称" prop="Module_Name">
          <a-input v-model="entity.Module_Name" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="父级模组" prop="Parent_Module_Id">
          <a-tree-select
            v-model="entity.Parent_Module_Id"
            allowClear
            :treeData="ParentIdTreeData"
            placeholder="请选择上级模组"
            treeDefaultExpandAll
          ></a-tree-select>
        </a-form-model-item>
        <a-form-model-item label="排序" prop="Sort">
          <a-input v-model="entity.Sort" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="说明" prop="Remark">
          <a-input v-model="entity.Remark" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: {},
      ParentIdTreeData: [],
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
      this.$http.post('/MiniPrograms/mini_mainpage_module/GetTreeDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.ParentIdTreeData = resJson.Data
        }
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/MiniPrograms/mini_mainpage_module/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/MiniPrograms/mini_mainpage_module/SaveData', this.entity).then(resJson => {
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
